using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class StackTests
    {
        [Test]
        public void Count_WhenCreated_Is0()
        {
            var stack = new Stack<object>();
            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Push_NullParam_ThrowsException()
        {
            var stack = new Stack<object>();
            Assert.That(
                    () => stack.Push(null),
                    Throws.ArgumentNullException
                );
        }

        [Test]
        public void Push_WhenCalled_AddToStack()
        {
            var stack = new Stack<int>();

            stack.Push(1);

            Assert.That(stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Pop_ListEmpty_ThrowsException()
        {
            var stack = new Stack<object>();
            Assert.That(
                    () => stack.Pop(),
                    Throws.InvalidOperationException
                );
        }

        [Test]
        public void Pop_HasOneItem_RemovesItem()
        {
            var stack = new Stack<int>();
            stack.Push(1);

            var result = stack.Pop();

            Assert.That(result, Is.EqualTo(1));
            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Pop_HasMultipleItems_RemovesTopItem()
        {
            var stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            var result = stack.Pop();

            Assert.That(result, Is.EqualTo(3));
            Assert.That(stack.Count, Is.EqualTo(2));
        }

        [Test]
        public void Peek_EmptyList_ThrowsException()
        {
            var stack = new Stack<object>();
            Assert.That(
                    () => stack.Peek(),
                    Throws.InvalidOperationException
                );
        }

        [Test]
        public void Peek_HasOneItem_ReturnsItem()
        {
            var stack = new Stack<int>();
            stack.Push(1);

            var result = stack.Peek();

            Assert.That(result, Is.EqualTo(1));
            Assert.That(stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Peek_HasMultipleItems_ReturnsTopItem()
        {
            var stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            var result = stack.Peek();

            Assert.That(result, Is.EqualTo(3));
            Assert.That(stack.Count, Is.EqualTo(3));
        }
    }
}
