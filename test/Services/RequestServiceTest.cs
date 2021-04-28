using System.Linq;
using csharp.services.scoped.impl;
using Xunit;

namespace test.Services
{
    public class RequestServiceTest
    {
        [Fact]
        public void CanCallEndpoint()
        {
            RequestService()
            Assert.True(1 == 2);
        }
    }
}