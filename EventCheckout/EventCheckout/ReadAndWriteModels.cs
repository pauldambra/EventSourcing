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

        [Fact]
        public void ADifferentCommandTurnsIntoADifferentEvent()
        {
            var eventStream = new EventStream();
            var basket = new Basket(eventStream);
            basket.ScanItem("Screwdriver");
            basket.VoidItem("Screwdriver");

            Assert.Equal(2, eventStream.Events.Count);
            Assert.True(eventStream.Events.Last() is ItemVoided);
        }

        [Fact]
        public void ACommandThatFailsBusinessLogicRaisesNoEvents()
        {
            var eventStream = new EventStream();
            var basket = new Basket(eventStream); 
            basket.VoidItem("Screwdriver");  

            Assert.Equal(0, eventStream.Events.Count);
        }
    }

    public class ItemVoided : IEvent
    {
        public ItemVoided(string sku)
        {
            Sku = sku;
        }

        public string Sku { get; }
    }

    public class ItemAdded : IEvent
    {
        public ItemAdded(string sku)
        {
            Sku = sku;
        }

        public string Sku { get; }
    }

    internal class Basket
    {
        private EventStream eventStream;
        private int NumberOfItems;

        public Basket(EventStream eventStream)
        {
            this.eventStream = eventStream;
        }

        internal void ScanItem(string sku)
        {
            var itemAdded = new ItemAdded(sku);
            NumberOfItems++;
            eventStream.Add(itemAdded);
        }

        internal void VoidItem(string sku)
        {
            if (NumberOfItems > 0)
            {
                var itemVoided = new ItemVoided(sku);
                eventStream.Add(itemVoided);
                NumberOfItems--;
            }
        }
    }

    public interface IEvent { }

    internal class EventStream
    {
        public EventStream()
        {
        }

        public List<IEvent> Events { get; internal set; } = new List<IEvent>();

        internal void Add(IEvent evt)
        {
            Events.Add(evt);
        }
    }

    public class ReadModelTests
    {
        [Fact]
        // the event stream can be used to generate a running total
        public void AnEventBecomesState()
        {
        }

        [Fact]
        // the event stream can be used to keep the running total up-to-date
        public void ReadModelsCanSubscribeToTheStream()
        {
        }
    }



}


