using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NUnit.Framework;

namespace mapmyfitnessapi_sdk.unit.tests
{
    [TestFixture]
    public class SandboxTests
    {

        [Test]
        public void TestMappingExpando()
        {
            // Arrange
            var json = "{\"id\": 1,\"foo\":\"two\"}";

            // Act
            var result = JsonConvert.DeserializeObject<dynamic>(json);

            // Assert
            int id = result.id;
            string foo = result.foo;
            Assert.That(id,Is.EqualTo(1));
            Assert.That(foo,Is.EqualTo("two"));
        } 

    }
}