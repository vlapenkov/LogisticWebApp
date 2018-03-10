using Logistic.Data;
using Logistic.Utils;
using Logistic.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Xunit;

namespace Logistic.Tests
{
    public class UnitTestEnums
    {
        [Fact]
        public void TestEnum_ReturnsDisplayAttribute()
        {
            Assert.Equal("на тендере", StatusOfClaim.OnTender.GetDisplayName());
        }
    }
}
        