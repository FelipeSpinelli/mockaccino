using Microsoft.VisualStudio.TestTools.UnitTesting;
using MrMime.Core.Tests.Models;

namespace MrMime.Core.Tests
{
    [TestClass]
    public class Tests_Mimenator
    {
        [TestMethod]
        public void Imitate_Success()
        {
            var mimenator = new Mimenator();
            mimenator.Load();

            var customer = mimenator.Imitate(new User(), "User");
            Assert.IsNotNull(customer);
        }
    }
}
