using Xunit;
using System.Collections.Generic;
using System.Linq;
using System;

namespace EventCheckout.Tests
{

    public class WriteModelTests
    {
        [Fact]
        // a command can be written to the event stream
        public void ACommandBecomesAnEvent()
        {
            var eventStream = new EventStream();
            var basket = new Basket(eventStream);
            basket.ScanItem("Screwdriver");
            Assert.Equal(1, eventStream.Events.Count);
            Assert.True(eventStream.Events.First() is ItemAdded);
        }
    }

    public class ItemAdded{
        public ItemAdded(string sku)
        {
            Sku = sku;
        }

        public string Sku { get; }
    }

    internal class Basket
    {
        private EventStream eventStream;

        public Basket(EventStream eventStream)
        {
            this.eventStream = eventStream;
        }

        internal void ScanItem(string sku)
        {
            var itemAdded = new ItemAdded(sku);
            eventStream.Add(itemAdded);
        }
    }

    internal class EventStream
    {
        public EventStream()
        {
        }

        public List<ItemAdded> Events { get; internal set; } = new List<ItemAdded>();

        internal void Add(ItemAdded evt)
        {
            Events.Add(evt);
        }
    }

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



}


