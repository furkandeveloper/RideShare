using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Text;

namespace RideShare.UnitTest.Static
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute(int count = 3)
            : base(() => new Fixture { RepeatCount = count, }.Customize(new AutoMoqCustomization()))
        {
        }
    }
}
