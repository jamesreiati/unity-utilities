using System;
using NUnit.Framework;
using Reiati.Utilities;

namespace Reiati.Utilities.Tests
{
    public class FutureTests
    {
        [Test]
        public void Constructor_SetsProperties()
        {
            // act
            Future future = new Future();

            // assert
            Assert.That(future.HasFulfilled, Is.False);
        }

        [Test]
        public void ConstructorBool_WithTrue_SetsProperties()
        {
            // act
            Future future = new Future(true);

            // assert
            Assert.That(future.HasFulfilled, Is.True);
        }

        [Test]
        public void ConstructorBool_WithFalse_SetsProperties()
        {
            // act
            Future future = new Future(false);

            // assert
            Assert.That(future.HasFulfilled, Is.False);
        }

        [Test]
        public void Fulfill_SetsHasFulfilled()
        {
            // arrange
            Future future = new Future();

            // act
            future.Fulfill();

            // assert
            Assert.That(future.HasFulfilled, Is.True);
        }

        [Test]
        public void Fulfill_RaisesEvent()
        {
            // arrange
            bool eventRaised = false;
            Future future = new Future();
            future.OnFulfilled += () =>
            {
                eventRaised = true;
            };

            // act
            future.Fulfill();

            // assert
            Assert.That(eventRaised, Is.True);
        }

        [Test]
        public void Fulfill_Twice_Throws()
        {
            // arrange
            Future future = new Future();
            future.Fulfill();

            // act
            void act()
            {
                future.Fulfill();
            }

            // assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Test]
        public void EventSubscribe_WithFulfilled_ImmediatelyInvokes()
        {
            // arrange
            bool eventRaised = false;
            Future future = new Future(true);

            // act
            future.OnFulfilled += () =>
            {
                eventRaised = true;
            };

            // assert
            Assert.That(eventRaised, Is.True);
        }

        [Test]
        public void ClearAndFulfillIfUnfulfilled_WithUnfulfilled_Fulfills()
        {
            // arrange
            bool eventRaised = false;
            Future future = new Future();
            future.OnFulfilled += () =>
            {
                eventRaised = true;
            };

            // act
            var wasUpdated = Future.ClearAndFulfillIfUnfulfilled(ref future);

            // assert
            Assert.That(eventRaised, Is.True);
            Assert.That(wasUpdated, Is.True);
            Assert.That(future, Is.Null);
        }

        [Test]
        public void ClearAndFulfillIfUnfulfilled_WithFulfilled_Clears()
        {
            // arrange
            Future future = new Future();
            future.Fulfill();

            // act
            var wasUpdated = Future.ClearAndFulfillIfUnfulfilled(ref future);

            // assert
            Assert.That(wasUpdated, Is.False);
            Assert.That(future, Is.Null);
        }

        [Test]
        public void ClearAndFulfillIfUnfulfilled_WithNull_Noops()
        {
            // arrange
            Future future = null;

            // act
            var wasUpdated = Future.ClearAndFulfillIfUnfulfilled(ref future);

            // assert
            Assert.That(wasUpdated, Is.False);
            Assert.That(future, Is.Null);
        }
    }
}
