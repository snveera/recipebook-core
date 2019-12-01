using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace recipebook.functions.test.TestUtility.Fakes
{
    public class FakeHttpContextAccessor : IHttpContextAccessor
    {
        private readonly TestContext _context;

        public FakeHttpContextAccessor(TestContext context)
        {
            _context = context;

            this.HttpContext = new DefaultHttpContext { User = context.CurrentPrincipal };

        }
        public HttpContext HttpContext { get; set; }
    }
}
