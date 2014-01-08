namespace Arrival.Tests
{
    using System;
    using NUnit.Framework;
    using Arrival;

    [TestFixture]
    public class ExportableTests
    {
        [Test]
        public void CacheIsLazyLoaded()
        {
//            var beforeCallCount = Exportable.propertiesCache.Count;
//            Assert.That(beforeCallCount, Is.EqualTo(0));

            var props = Exportable.ValidProperties(typeof(Exportee), "C", "A");

            Assert.That(props.Count, Is.EqualTo(2));
			Assert.That(props[0].Name, Is.EqualTo("C"));
        }
    }

    public class Exportee
    {
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
    }
}

