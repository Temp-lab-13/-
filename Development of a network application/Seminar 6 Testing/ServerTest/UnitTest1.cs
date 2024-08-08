using SeminarWork.Model;
using SeminarWork.Service;

namespace ServerTest
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class Test
    {
        [SetUp]
        public void Setup()
        {
            using (var ctx = new ChatContext())
            {
                ctx.Messages.RemoveRange(ctx.Messages);
                ctx.Users.RemoveRange(ctx.Users);
                ctx.SaveChanges();
            }
        }

        [TearDown]
        public void Teardown()
        {
            using (var ctx = new ChatContext())
            {
                ctx.Messages.RemoveRange(ctx.Messages);
                ctx.Users.RemoveRange(ctx.Users);
                ctx.SaveChanges();
            }
        }
        [Test]
        public async Task Test1()
        {
            var mock = new MockMessageSourse();
            var srv = new UDPServer(mock);
            mock.AddServer(srv);
            await srv.StartServer();

            using (var ctx = new ChatContext())
            {
                Assert.IsTrue(ctx.Users.Count() == 2, "������������ �� �������.");

                var user1 = ctx.Users.FirstOrDefault(x => x.FullName == "������");
                var user2 = ctx.Users.FirstOrDefault(x => x.FullName == "����");

                Assert.IsNotNull(user1, "������������ �� ������.");
                Assert.IsNotNull(user2, "������������ �� ������.");

                Assert.IsTrue(user1.messagesFrom.Count == 1);
                Assert.IsTrue(user2.messagesFrom.Count == 1);

                Assert.IsTrue(user1.messagesTo.Count == 1);
                Assert.IsTrue(user2.messagesTo.Count == 1);

                var msg1 = ctx.Messages.FirstOrDefault(x => x.UserFrom == user1 && x.UserTO == user2);
                var msg2 = ctx.Messages.FirstOrDefault(x => x.UserFrom == user2 && x.UserTO == user1);

                Assert.AreEqual("����������, �������.", msg2.Text);
                Assert.AreEqual("���!", msg1.Text);

            }
        }

    }  
}
