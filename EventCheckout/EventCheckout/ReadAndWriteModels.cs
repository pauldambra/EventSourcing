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
            var eventStream = new EventStream(new List<IEvent>());
            var basket = new Basket(eventStream);
            basket.ScanItem("Screwdriver");
            Assert.Equal(1, eventStream.Events.Count);
            Assert.True(eventStream.Events.First() is ItemAdded);
        }

        [Fact]
        public void ADifferentCommandTurnsIntoADifferentEvent()
        {
            var eventStream = new EventStream(new List<IEvent>());
            var basket = new Basket(eventStream);
            basket.ScanItem("Screwdriver");
            basket.VoidItem("Screwdriver");

            Assert.Equal(2, eventStream.Events.Count);
            Assert.True(eventStream.Events.Last() is ItemVoided);
        }

        [Fact]
        public void ACommandThatFailsBusinessLogicRaisesNoEvents()
        {
            var eventStream = new EventStream(new List<IEvent>());
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
        private Action<IEvent> OnEventAppeared;

        public EventStream(List<IEvent> list)
        {
            Events = list;
        }

        public List<IEvent> Events { get; internal set; }

        internal void Add(IEvent evt)
        {
            Events.Add(evt);
            if (OnEventAppeared != null){
                OnEventAppeared.Invoke(evt);
            }
        }

        internal List<IEvent> ReadStreamForwards()
        {
            return Events;
        }

        internal void Subscribe(Action<IEvent> callback)
        {
            OnEventAppeared = callback;
        }
    }

    public class ReadModelTests
    {
        [Fact]
        // the event stream can be used to generate a running total
        public void AnEventBecomesState()
        {
            var eventStream = new EventStream(new List<IEvent>(){
                new ItemAdded("Screwdriver"),
                new ItemAdded("Hammer")
            });

            var basketItemCount = new BasketItemCount(eventStream);

            Assert.Equal(2, basketItemCount.Value);

            eventStream.Add(new ItemVoided("Screwdriver"));

            Assert.Equal(1, basketItemCount.Value);

        }

        [Fact]
        // the event stream can be used to keep the running total up-to-date
        public void ReadModelsCanSubscribeToTheStream()
        {
        }
    }

    internal class BasketItemCount
    {
        public BasketItemCount(EventStream eventStream)
        {
            var events = eventStream.ReadStreamForwards();
            eventStream.Subscribe((evt)=> {
                if (evt is ItemVoided){
                    Value--;
                }
                else {
                    Value++;
                }
            });
            Value = events.Count;
        }

        

        

        public int Value { get; internal set; }
    }
}


