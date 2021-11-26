using System;
using System.Collections.Generic;
using System.Text;

namespace WebChat.Common.Utilities.Constants
{
    public class SystemConstant
    {
        public const string MainConnectionString = "WebChatDb";

        public const string UserName = "UserName";

        public const string UserId = "UserId";

        public const string ChatHub = "/chathub";

        public const int KeepAliveIntervalSeconds = 1800; // 1800s

        public const int ClientTimeoutIntervalSeconds = KeepAliveIntervalSeconds * 2; // 3600s

        public const int PageSize = 10;

        public const int LifeTimeConfirmEmail = 48; // 48 giờ

        public static readonly string[] ImageExtension = { ".png", ".jpg", ".gif", ".jpeg", ".bmp", ".ico", ".webp" };

        public static readonly string[] VideoExtension = { ".avi", ".flv", ".mov", ".mp4", ".mpg", ".wmv" };

        public const long MaximumSizeFile = 20 * 1024 * 1024; // 20MB

        public const string DefaultGroupAvatar = "\\user-content\\img\\default-group.png";

        public const string DefaultUserAvatar = "\\user-content\\img\\default-avatar.png";

        public const string DefaultNameGroup = "Anonymous";

    }
}
