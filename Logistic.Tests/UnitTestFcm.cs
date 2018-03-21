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
using Logistic.Web.Services;
using Xunit;
using System.Diagnostics;

namespace Logistic.Tests
{
    public class UnitTestFcm
    {
        [Fact]
        public void TestEnum_ReturnsDisplayAttribute()
        {
           string data= new FcmService().SendNotification("test title", "dM6JgJnd2MI:APA91bFIIlwsKsYjQVqhMsffEYoz9bIrR6HJyVzppXOt4WmfrlB11ovrHLV5ZLN-ClPergBsaidlsBnQAluc4uq9ewI_4s9c6blxs-zxgCNvz4gCuaz6uZLfNA-Y6QGDjGskmgR8cYWT");

            Debug.WriteLine("data:" + data);
            Assert.Equal("data", "");
        }
    }
}
        