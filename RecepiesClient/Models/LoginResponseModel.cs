﻿using System;

namespace RecepiesClient.Models
{
    public class LoginResponseModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string AccessToken { get; set; }
    }
}