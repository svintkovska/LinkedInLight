﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.AuthModels
{
    public class ChangePasswordVM
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string Email { get; set; }
    }
}
