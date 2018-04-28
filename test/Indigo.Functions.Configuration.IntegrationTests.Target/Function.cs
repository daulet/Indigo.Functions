using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Globalization;

namespace Indigo.Functions.Configuration.IntegrationTests.Target
{
    public static class Function
    {
        [FunctionName("NoSettingNameFunction")]
        public static IActionResult NoSettingNameFunction(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "NoSettingName")] HttpRequest req,
            [Config] string value,
            TraceWriter log)
        {
            return new OkObjectResult($"Parsed {value}");
        }

        [FunctionName("BoolFunction")]
        public static IActionResult BoolFunction(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Bool")] HttpRequest req,
            [Config("Bool")] bool value,
            TraceWriter log)
        {
            if (value != true)
            {
                throw new ArgumentException("Couldn't parse the value");
            }
            return new OkObjectResult($"Parsed {value}");
        }

        [FunctionName("ByteFunction")]
        public static IActionResult ByteFunction(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Byte")] HttpRequest req,
            [Config("Byte")] Byte value,
            TraceWriter log)
        {
            if (value != Byte.MaxValue)
            {
                throw new ArgumentException("Couldn't parse the value");
            }
            return new OkObjectResult($"Parsed {value}");
        }

        [FunctionName("SByteFunction")]
        public static IActionResult SByteFunction(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "SByte")] HttpRequest req,
            [Config("SByte")] SByte value,
            TraceWriter log)
        {
            if (value != SByte.MaxValue)
            {
                throw new ArgumentException("Couldn't parse the value");
            }
            return new OkObjectResult($"Parsed {value}");
        }

        [FunctionName("CharFunction")]
        public static IActionResult CharFunction(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Char")] HttpRequest req,
            [Config("Char")] Char value,
            TraceWriter log)
        {
            if (value != 'X')
            {
                throw new ArgumentException("Couldn't parse the value");
            }
            return new OkObjectResult($"Parsed {value}");
        }

        [FunctionName("DateTimeFunction")]
        public static IActionResult DateTimeFunction(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "DateTime")] HttpRequest req,
            [Config("DateTime")] DateTime value,
            TraceWriter log)
        {
            if (value != DateTime.Parse("2018-4-13 14:00:00", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal))
            {
                throw new ArgumentException("Couldn't parse the value");
            }
            return new OkObjectResult($"Parsed {value}");
        }

        [FunctionName("DateTimeOffsetFunction")]
        public static IActionResult DateTimeOffsetFunction(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "DateTimeOffset")] HttpRequest req,
            [Config("DateTimeOffset")] DateTimeOffset value,
            TraceWriter log)
        {
            if (value != DateTimeOffset.Parse("2018-4-13 14:00:00", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal))
            {
                throw new ArgumentException("Couldn't parse the value");
            }
            return new OkObjectResult($"Parsed {value}");
        }

        [FunctionName("DecimalFunction")]
        public static IActionResult DecimalFunction(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Decimal")] HttpRequest req,
            [Config("Decimal")] Decimal value,
            TraceWriter log)
        {
            if (value != Decimal.MaxValue)
            {
                throw new ArgumentException("Couldn't parse the value");
            }
            return new OkObjectResult($"Parsed {value}");
        }

        [FunctionName("DoubleFunction")]
        public static IActionResult DoubleFunction(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Double")] HttpRequest req,
            [Config("Double")] Double value,
            TraceWriter log)
        {
            if (value != 12345.67890123456)
            {
                throw new ArgumentException("Couldn't parse the value");
            }
            return new OkObjectResult($"Parsed {value}");
        }

        [FunctionName("FloatFunction")]
        public static IActionResult FloatFunction(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Float")] HttpRequest req,
           [Config("Float")] float value,
           TraceWriter log)
        {
            if (value != 123.456789f)
            {
                throw new ArgumentException("Couldn't parse the value");
            }
            return new OkObjectResult($"Parsed {value}");
        }

        [FunctionName("GuidFunction")]
        public static IActionResult GuidFunction(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Guid")] HttpRequest req,
            [Config("Guid")] Guid value,
            TraceWriter log)
        {
            if (value != Guid.Parse("e20f2d60-3174-433f-a817-1131e9338978"))
            {
                throw new ArgumentException("Couldn't parse the value");
            }
            return new OkObjectResult($"Parsed {value}");
        }

        [FunctionName("IntFunction")]
        public static IActionResult IntFunction(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Int")] HttpRequest req,
           [Config("Int")] int value,
           TraceWriter log)
        {
            if (value != int.MaxValue)
            {
                throw new ArgumentException("Couldn't parse the value");
            }
            return new OkObjectResult($"Parsed {value}");
        }

        [FunctionName("UintFunction")]
        public static IActionResult UintFunction(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Uint")] HttpRequest req,
           [Config("Uint")] uint value,
           TraceWriter log)
        {
            if (value != uint.MaxValue)
            {
                throw new ArgumentException("Couldn't parse the value");
            }
            return new OkObjectResult($"Parsed {value}");
        }

        [FunctionName("LongFunction")]
        public static IActionResult LongFunction(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Long")] HttpRequest req,
           [Config("Long")] long value,
           TraceWriter log)
        {
            if (value != long.MaxValue)
            {
                throw new ArgumentException("Couldn't parse the value");
            }
            return new OkObjectResult($"Parsed {value}");
        }

        [FunctionName("ULongFunction")]
        public static IActionResult ULongFunction(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "ULong")] HttpRequest req,
           [Config("ULong")] ulong value,
           TraceWriter log)
        {
            if (value != ulong.MaxValue)
            {
                throw new ArgumentException("Couldn't parse the value");
            }
            return new OkObjectResult($"Parsed {value}");
        }

        [FunctionName("ShortFunction")]
        public static IActionResult ShortFunction(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Short")] HttpRequest req,
           [Config("Short")] short value,
           TraceWriter log)
        {
            if (value != short.MaxValue)
            {
                throw new ArgumentException("Couldn't parse the value");
            }
            return new OkObjectResult($"Parsed {value}");
        }

        [FunctionName("UShortFunction")]
        public static IActionResult UShortFunction(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "UShort")] HttpRequest req,
           [Config("UShort")] ushort value,
           TraceWriter log)
        {
            if (value != ushort.MaxValue)
            {
                throw new ArgumentException("Couldn't parse the value");
            }
            return new OkObjectResult($"Parsed {value}");
        }

        [FunctionName("StringFunction")]
        public static IActionResult StringFunction(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "String")] HttpRequest req,
            [Config("String")] String value,
            TraceWriter log)
        {
            if (value != "abc")
            {
                throw new ArgumentException("Couldn't parse the value");
            }
            return new OkObjectResult($"Parsed {value}");
        }

        [FunctionName("TimespanFunction")]
        public static IActionResult TimeSpanFunction(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "TimeSpan")] HttpRequest req,
            [Config("TimeSpan")] TimeSpan value,
            TraceWriter log)
        {
            if (value !=TimeSpan
                .FromDays(6)
                .Add(TimeSpan.FromHours(12))
                .Add(TimeSpan.FromMinutes(14))
                .Add(TimeSpan.FromSeconds(45)))
            {
                throw new ArgumentException("Couldn't parse the value");
            }
            return new OkObjectResult($"Parsed {value}");
        }
    }
}
