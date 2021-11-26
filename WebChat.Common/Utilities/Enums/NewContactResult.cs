using System;
using System.Collections.Generic;
using System.Text;

namespace WebChat.Common.Utilities.Enums
{
    public enum NewContactResult
    {
        Error, // 0 - lỗi
        Successed, // 1 - thêm thành công
        Exist // 2 - đã tồn tại
    }
}
