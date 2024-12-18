using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleForms
{
    /// <summary>
    ///  Specifies the initial position of a form.
    /// </summary>
    public enum FormStartPosition
    {
        /// <summary>
        ///  The location and size of the form will determine its starting position.
        /// </summary>
        Manual = 0,

        /// <summary>
        ///  The form is centered on the current display, and has the dimensions
        ///  specified in the form's size.
        /// </summary>
        CenterScreen = 1,

        /// <summary>
        ///  The form is positioned at the Windows default location and has the
        ///  dimensions specified in the form's size.
        /// </summary>
        WindowsDefaultLocation = 2,

        /// <summary>
        ///  The form is positioned at the Windows default location and has the
        ///  bounds determined by Windows default.
        /// </summary>
        WindowsDefaultBounds = 3,

        /// <summary>
        ///  The form is centered within the bounds of its parent form.
        /// </summary>
        CenterParent = 4
    }

    /// <summary>
    ///  Specifies the border styles for a form.
    /// </summary>
    public enum FormBorderStyle
    {
        /// <summary>
        ///  No border.
        /// </summary>
        None = 0,

        /// <summary>
        ///  A fixed, single line border.
        /// </summary>
        FixedSingle = 1,

        /// <summary>
        ///  A fixed, three-dimensional border.
        /// </summary>
        Fixed3D = 2,

        /// <summary>
        ///  A thick, fixed dialog-style border.
        /// </summary>
        FixedDialog = 3,

        /// <summary>
        ///  A resizable border.
        /// </summary>
        Sizable = 4,

        /// <summary>
        ///  A tool window border that is not resizable.
        /// </summary>
        FixedToolWindow = 5,

        /// <summary>
        ///  A resizable tool window border.
        /// </summary>
        SizableToolWindow = 6,
    }

    /// <summary>
    ///  Specifies how a form window is displayed.
    /// </summary>
    public enum FormWindowState
    {
        /// <summary>
        ///  A default sized window.
        /// </summary>
        Normal = 0,

        /// <summary>
        ///  A minimized window.
        /// </summary>
        Minimized = 1,

        /// <summary>
        ///  A maximized window.
        /// </summary>
        Maximized = 2,
    }

    /// <summary>
    ///  Specifies the reason for the Form Closing.
    /// </summary>
    public enum CloseReason
    {
        /// <summary>
        ///  No reason for closure of the Form.
        /// </summary>
        None = 0,

        /// <summary>
        ///  In the process of shutting down, Windows has closed the application.
        /// </summary>
        WindowsShutDown = 1,

        /// <summary>
        ///  The parent form of this MDI form is closing.
        /// </summary>
        MdiFormClosing = 2,

        /// <summary>
        ///  The user has clicked the close button on the form window, selected
        ///  Close from the window's control menu or hit Alt + F4.
        /// </summary>
        UserClosing = 3,

        /// <summary>
        ///  The Microsoft Windows Task Manager is closing the application.
        /// </summary>
        TaskManagerClosing = 4,

        /// <summary>
        ///  A form is closing because its owner is closing.
        /// </summary>
        FormOwnerClosing = 5,

        /// <summary>
        ///  A form is closing because Application.Exit() was called.
        /// </summary>
        ApplicationExitCall = 6
    }
}
