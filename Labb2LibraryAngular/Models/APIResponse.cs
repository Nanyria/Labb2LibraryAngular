﻿using System.Net;

namespace Labb2LibraryAngular.Models
{
    public class APIResponse
    {
        public APIResponse() 
        {
            ErrorMessages= new List<string>();
        }
        public bool IsSuccess { get; set; }
        public Object Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string> ErrorMessages { get; set; }
    }


}
