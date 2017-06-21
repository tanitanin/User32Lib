using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace User32Lib
{
    public static partial class User32
    {

        /// <summary>
        /// Registers the application to receive power setting notifications for the specific power setting event.
        /// </summary>
        /// <param name="hRecipient">Handle indicating where the power setting notifications are to be sent. For interactive applications, the Flags parameter should be zero, and the hRecipient parameter should be a window handle. For services, the Flags parameter should be one, and the hRecipient parameter should be a SERVICE_STATUS_HANDLE as returned from RegisterServiceCtrlHandlerEx.</param>
        /// <param name="PowerSettingGuid">The GUID of the power setting for which notifications are to be sent. For more information see Registering for Power Events.</param>
        /// <param name="Flags"></param>
        /// <returns>Returns a notification handle for unregistering for power notifications. If the function fails, the return value is NULL. To get extended error information, call GetLastError.</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr RegisterPowerSettingNotification(
            IntPtr  hRecipient,
            Guid PowerSettingGuid,
            [MarshalAs(UnmanagedType.U4)] DeviceNotify Flags
        );

        /// <summary>
        /// Registers to receive notification when the system is suspended or resumed. Similar to PowerRegisterSuspendResumeNotification, but operates in user mode and can take a window handle.
        /// </summary>
        /// <param name="hRecipient"></param>
        /// <param name="Flags"></param>
        /// <returns>A handle to the registration. Use this handle to unregister for notifications. If the function fails, the return value is NULL. To get extended error information call GetLastError.</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr RegisterSuspendResumeNotification(
           IntPtr hRecipient,
           [MarshalAs(UnmanagedType.U4)] DeviceNotify Flags
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr RegisterSuspendResumeNotification(
           [MarshalAs(UnmanagedType.LPStruct)] ref DeviceNotifySubscribeParameters hRecipient,
           [MarshalAs(UnmanagedType.U4)] DeviceNotify Flags
        );

        /// <summary>
        /// Unregisters the power setting notification.
        /// </summary>
        /// <param name="Handle">The handle returned from the RegisterPowerSettingNotification function.</param>
        /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool UnregisterPowerSettingNotification(IntPtr Handle);


        /// <summary>
        /// Cancels a registration to receive notification when the system is suspended or resumed. Similar to PowerUnregisterSuspendResumeNotification but operates in user mode.
        /// </summary>
        /// <param name="RegistrationHandle">A handle to a registration obtained by calling the RegisterSuspendResumeNotification function.</param>
        /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool UnregisterSuspendResumeNotification(IntPtr RegistrationHandle);

    }

    [Flags]
    public enum DeviceNotify : UInt32
    {
        DEVICE_NOTIFY_WINDOW_HANDLE = 0,
        DEVICE_NOTIFY_SERVICE_HANDLE = 1,
        DEVICE_NOTIFY_CALLBACK,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DeviceNotifySubscribeParameters
    {
        [MarshalAs(UnmanagedType.FunctionPtr)] DeviceNotifyCallbackRoutine Callback;
        [MarshalAs(UnmanagedType.IUnknown)] object Context;
    }

    public delegate ulong DeviceNotifyCallbackRoutine(
        [MarshalAs(UnmanagedType.IUnknown)] object Context,
        ulong Type,
        [MarshalAs(UnmanagedType.IUnknown)] object Setting
    );

}
