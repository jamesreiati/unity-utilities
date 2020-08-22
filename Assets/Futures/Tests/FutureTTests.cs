using System;
using NUnit.Framework;
using Reiati.Utilities;

namespace Reiati.Utilities.Tests
{
    public class FutureTTests
    {
        [Test]
        public void Constructor_SetsProperties()
        {
            // act
            Future<string> future = new Future<string>();

            // assert
            Assert.That(future.HasFulfilled, Is.False);
        }

        [Test]
        public void ConstructorT_SetsProperties()
        {
            // arrange
            string value = "test";

            // act
            Future<string> future = new Future<string>(value);

            // assert
            Assert.That(future.HasFulfilled, Is.True);
            Assert.That(future.Value, Is.EqualTo(value));
        }

        [Test]
        public void Fulfill_SetsProperties()
        {
            // arrange
            string value = "test";
            Future<string> future = new Future<string>();

            // act
            future.Fulfill(value);

            // assert
            Assert.That(future.HasFulfilled, Is.True);
            Assert.That(future.Value, Is.EqualTo(value));
        }

        [Test]
        public void Fulfill_RaisesEvent()
        {
            // arrange
            string value = "test";
            string eventValue = null;
            Future<string> future = new Future<string>();
            future.OnFulfilled += (val) =>
            {
                eventValue = val;
            };

            // act
            future.Fulfill(value);

            // assert
            Assert.That(eventValue, Is.EqualTo(value));
        }

        [Test]
        public void Fulfill_Twice_Throws()
        {
            // arrange
            string value = "test";
            Future<string> future = new Future<string>();
            future.Fulfill(value);

            // act
            void act()
            {
                future.Fulfill(value);
            }

            // assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Test]
        public void EventSubscribe_WithFulfilled_ImmediatelyInvokes()
        {
            // arrange
            string value = "test";
            string eventValue = null;
            Future<string> future = new Future<string>(value);

            // act
            future.OnFulfilled += (val) =>
            {
                eventValue = val;
            };

            // assert
            Assert.That(eventValue, Is.EqualTo(eventValue));
        }
    }
}
