using System;
using System.Drawing;
using System.Windows.Forms;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;

namespace POS_Shop.Helpers
{
    public static class ToastHelper
    {
        private static Notifier _notifier;

        static ToastHelper()
        {
            _notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new PrimaryScreenPositionProvider(
                    corner: Corner.BottomRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(2),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(2));

                cfg.DisplayOptions.TopMost = true;
                cfg.DisplayOptions.Width = 300;
            });
        }

        public static void ShowSuccess(string message)
        {
            ShowNotification("Success", message, ToolTipIcon.None);
        }

        public static void ShowError(string message)
        {
            ShowNotification("Error", message, ToolTipIcon.Error);
        }

        public static void ShowInfo(string message)
        {
            ShowNotification("Info", message, ToolTipIcon.Info);
        }

        public static void ShowWarning(string message)
        {
            ShowNotification("Warning", message, ToolTipIcon.Warning);
        }

        public static void Dispose()
        {
            _notifier?.Dispose();
            _notifier = null;
        }

        private static void ShowNotification(string title, string message, ToolTipIcon icon)
        {
            // Using NotifyIcon for toast-like notifications
            using (var notifyIcon = new NotifyIcon())
            {
                notifyIcon.Icon = SystemIcons.Application; // Or your app icon
                notifyIcon.Visible = true;
                notifyIcon.BalloonTipTitle = title;
                notifyIcon.BalloonTipText = message;
                notifyIcon.BalloonTipIcon = icon;
                notifyIcon.ShowBalloonTip(2000); // 2 seconds
            }
        }
    }
}
