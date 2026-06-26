

select * from Products 
select * from ProductPrices 

select * from OrderDetails




-- Simple query for product IDs 2 and 3
DECLARE @FromDate DATETIME = '2025-01-01'; -- Set your desired date
DECLARE @ToDate DATETIME = '2026-06-20';   -- Set your desired date

WITH ProductUnitMapping AS (
    SELECT 
        pp.ProductId,
        pp.Unit,
        pp.ItemsCount,
        ROW_NUMBER() OVER (PARTITION BY pp.ProductId, pp.Unit ORDER BY pp.CreatedDate DESC) AS rn
    FROM ProductPrices pp
    WHERE pp.ProductId IN (2, 3)
),
ProductSales AS (
    SELECT 
        od.ProductId,
        MAX(p.ProductEnglishName) AS ProductEnglishName,
        MAX(p.ProductUrduName) AS ProductUrduName,
        SUM(
            CASE 
                WHEN pum.ItemsCount IS NOT NULL 
                THEN od.Quantity * pum.ItemsCount
                ELSE od.Quantity 
            END
        ) AS TotalPiecesSold,
        SUM(od.Quantity) AS RawQuantity,
        COUNT(DISTINCT od.OrderId) AS TotalOrders,
        SUM(od.Price * od.Quantity) AS TotalRevenue,
        MIN(od.CreatedDate) AS FirstSaleDate,
        MAX(od.CreatedDate) AS LastSaleDate
    FROM OrderDetails od WITH (NOLOCK)
    INNER JOIN Products p WITH (NOLOCK) ON od.ProductId = p.Id
    LEFT JOIN ProductUnitMapping pum ON od.ProductId = pum.ProductId 
        AND od.QuantityType = pum.Unit
        AND pum.rn = 1
    WHERE od.ProductId IN (2, 3)
        AND od.CreatedDate >= @FromDate
        AND od.CreatedDate <= DATEADD(day, 1, @ToDate)
        AND od.ProductId IS NOT NULL
        AND od.Quantity > 0
    GROUP BY od.ProductId
),
QuantityBreakdown AS (
    SELECT 
        od.ProductId,
        STRING_AGG(
            CONCAT(
                ISNULL(od.QuantityType, 'Pcs'), 
                ': ', 
                CAST(od.TotalQty AS VARCHAR(10))
            ), 
            ', '
        ) WITHIN GROUP (ORDER BY od.TotalQty DESC) AS Breakdown
    FROM (
        SELECT 
            ProductId,
            QuantityType,
            SUM(Quantity) AS TotalQty
        FROM OrderDetails
        WHERE ProductId IN (2, 3)
            AND CreatedDate >= @FromDate
            AND CreatedDate <= DATEADD(day, 1, @ToDate)
        GROUP BY ProductId, QuantityType
    ) od
    GROUP BY od.ProductId
)
SELECT 
    ROW_NUMBER() OVER (ORDER BY ISNULL(ps.TotalPiecesSold, 0) DESC) AS Rank,
    p.Id AS Id,
    p.ProductEnglishName AS ProductName,
    ISNULL(ps.TotalPiecesSold, 0) AS QtySoldInPieces,
    ISNULL(ps.RawQuantity, 0) AS RawQuantity,
    ISNULL(ps.TotalOrders, 0) AS TotalOrders,
    ISNULL(ps.TotalRevenue, 0) AS TotalRevenue,
    ISNULL(ps.FirstSaleDate, NULL) AS FirstSaleDate,
    ISNULL(ps.LastSaleDate, NULL) AS LastSaleDate,
    ISNULL(qb.Breakdown, 'No sales') AS QuantityBreakdown,
    CASE 
        WHEN ps.TotalPiecesSold IS NULL THEN 'No Sales'
        WHEN ps.TotalPiecesSold > 100 THEN 'High Seller'
        WHEN ps.TotalPiecesSold > 50 THEN 'Medium Seller'
        ELSE 'Low Seller'
    END AS PerformanceCategory
FROM Products p
LEFT JOIN ProductSales ps ON p.Id = ps.ProductId
LEFT JOIN QuantityBreakdown qb ON p.Id = qb.ProductId
WHERE p.Id IN (2, 3)
ORDER BY Rank;




-----------------------




-- Query for monthly aggregated line chart
DECLARE @FromDate DATETIME = '2025-01-01';
DECLARE @ToDate DATETIME = '2026-06-20';

WITH ProductUnitMapping AS (
    SELECT 
        pp.ProductId,
        pp.Unit,
        pp.ItemsCount,
        ROW_NUMBER() OVER (PARTITION BY pp.ProductId, pp.Unit ORDER BY pp.CreatedDate DESC) AS rn
    FROM ProductPrices pp
    WHERE pp.ProductId IN (2, 3)
),
MonthlySales AS (
    SELECT 
        DATEFROMPARTS(YEAR(od.CreatedDate), MONTH(od.CreatedDate), 1) AS MonthStart,
        od.ProductId,
        p.ProductEnglishName,
        p.ProductUrduName,
        SUM(
            CASE 
                WHEN pum.ItemsCount IS NOT NULL 
                THEN od.Quantity * pum.ItemsCount
                ELSE od.Quantity 
            END
        ) AS MonthlyPiecesSold,
        SUM(od.Quantity) AS MonthlyRawQuantity,
        COUNT(DISTINCT od.OrderId) AS MonthlyOrders,
        SUM(od.Price * od.Quantity) AS MonthlyRevenue
    FROM OrderDetails od WITH (NOLOCK)
    INNER JOIN Products p WITH (NOLOCK) ON od.ProductId = p.Id
    LEFT JOIN ProductUnitMapping pum ON od.ProductId = pum.ProductId 
        AND od.QuantityType = pum.Unit
        AND pum.rn = 1
    WHERE od.ProductId IN (2, 3)
        AND od.CreatedDate >= @FromDate
        AND od.CreatedDate <= DATEADD(day, 1, @ToDate)
        AND od.ProductId IS NOT NULL
        AND od.Quantity > 0
    GROUP BY DATEFROMPARTS(YEAR(od.CreatedDate), MONTH(od.CreatedDate), 1),
             od.ProductId, p.ProductEnglishName, p.ProductUrduName
)
SELECT 
    MonthStart,
    ProductId,
    ProductEnglishName,
    ProductUrduName,
    ISNULL(MonthlyPiecesSold, 0) AS MonthlyPiecesSold,
    ISNULL(MonthlyRawQuantity, 0) AS MonthlyRawQuantity,
    ISNULL(MonthlyOrders, 0) AS MonthlyOrders,
    ISNULL(MonthlyRevenue, 0) AS MonthlyRevenue
FROM MonthlySales
ORDER BY MonthStart, ProductId;