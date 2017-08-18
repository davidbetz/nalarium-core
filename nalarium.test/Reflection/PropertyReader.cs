#region Copyright

// MIT License

// Copyright (c) 2007-2017 David Betz

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

#endregion

using Xunit;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nalarium.Test
{
    public class Order
    {
        [Display(Name = "Order.OrderId", Description = "Order Order Id")]
        public string OrderId { get; set; }

        [Display(Name = "Order.LookupCode", Description = "Order Order Lookup Code")]
        public string LookupCode { get; set; }

        [Display(Name = "Order.ExpectedDate", Description = "Expected Date", Order = 1)]
        [DisplayFormat(DataFormatString = "M/D/yyyy")]
        public DateTime ExpectedDate { get; set; }

        [Display(Name = "Order.StatusName", Description = "Status")]
        public string StatusName { get; set; }

        [Display(Name = "Order.StatusId", Description = "Status Id")]
        public string StatusId { get; set; }
    }

    public class PropertyReader
    {
        [Fact]
        public void GetPropertyList()
        {
            var result = Nalarium.Reflection.PropertyReader.GetPropertyList(typeof(Order)).Select(p => p.Name);

            Assert.Equal(5, result.Count());
        }
    }
}
