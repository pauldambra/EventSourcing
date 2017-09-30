using Xunit;

namespace EventCheckout.Tests
{
    public class EventSourcedBasketTests
    {
        [Fact]
        public void AnATotalIsFifty()
        {
            Event[] events = {
                new ItemScanned("A", 50)
            };
            
            var basket = new Basket();
            basket.Apply(events);
            
            Assert.Equal(50, basket.Total);
        }

        internal class Basket
        {
            public void Apply(Event[] events)
            {
                throw new System.NotImplementedException();
            }

            public int Total { get; private set; }
        }

        internal class ItemScanned : Event
        {
            public ItemScanned(string sku, int price)
            {
                throw new System.NotImplementedException();
            }
        }

        internal class Event
        {
        }
    }
}