﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webAuth
{
    class globalData
    {
        public static string[] login_status_arr;
        public static string user_name, user_password;
        public static string login_cookie;
        public static bool keeplive_exit = false;
        public static bool auto_login = true;
        public static bool relogin = false;
    }
}
