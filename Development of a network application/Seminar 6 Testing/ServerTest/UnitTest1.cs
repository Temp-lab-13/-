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
                Assert.IsTrue(ctx.Users.Count() == 2, "Пользователи не созданы.");

                var user1 = ctx.Users.FirstOrDefault(x => x.FullName == "Кокоми");
                var user2 = ctx.Users.FirstOrDefault(x => x.FullName == "Сара");

                Assert.IsNotNull(user1, "Пользователь не создан.");
                Assert.IsNotNull(user2, "Пользователь не создан.");

                Assert.IsTrue(user1.messagesFrom.Count == 1);
                Assert.IsTrue(user2.messagesFrom.Count == 1);

                Assert.IsTrue(user1.messagesTo.Count == 1);
                Assert.IsTrue(user2.messagesTo.Count == 1);

                var msg1 = ctx.Messages.FirstOrDefault(x => x.UserFrom == user1 && x.UserTO == user2);
                var msg2 = ctx.Messages.FirstOrDefault(x => x.UserFrom == user2 && x.UserTO == user1);

                Assert.AreEqual("Сдавайтесь, генерал.", msg2.Text);
                Assert.AreEqual("Нет!", msg1.Text);

            }
        }

    }  
}
