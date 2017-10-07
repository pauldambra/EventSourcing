using Xunit;
using System.Collections.Generic;
using System.Linq;
using System;

namespace EventCheckout.Tests
{
 
   public class ReadModelTests
    {
        [Fact]
        // the event stream can be used to generate a running total
        public void AnEventBecomesState() {
        }

        [Fact]
        // the event stream can be used to keep the running total up-to-date
        public void ReadModelsCanSubscribeToTheStream() {
        }
    }

    public class WriteModelTests
    {
        [Fact]
        // a command can be written to the event stream
        public void ACommandBecomesAnEvent()
        {
        }
    }

}


