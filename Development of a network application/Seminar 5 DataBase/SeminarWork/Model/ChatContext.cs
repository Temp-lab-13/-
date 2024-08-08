using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SeminarWork.Model
{
    public class ChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ChatContext() { }

        public ChatContext(DbContextOptions<ChatContext> options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // ms sql/ То же не работает, так как у меня тупо падает контейнер с бд. И я без понятия почему, ведь всё что он пишет (контейнер остановлент, так как сервер закончил свою работу... что?)  
            optionsBuilder.UseSqlServer("Server=db:3306;Database=dbSeminar;Security=False;TrustServerCertificate=True").UseLazyLoadingProxies();


            // Не работает с MySql. 
            // Препробовал всё что можно. И вариант с UseSqlServer и тот что нашёл в интернете, через UseMySql.
            // Разные варианты server: localhost, localhost:3306, localhost:6033, . , db, db:3306(именно так у меня отображаетсяимя сервера в phpAminer). Без азницы.
            // Провоал зайти от root, а не от моего пользователя.
            // Пробовал прописывать отдельно Port=3306, и Port=6033. Без изменений.
            // Пробовал разный формат строки. не помогает.


            // Строка контекста с сайта: Server = myServerAddress; Database = myDataBase; Uid = myUsername; Pwd = myPassword;
            // optionsBuilder.UseMySql("Server=db;Database=dbSeminar;Uid=kro;Pwd=ex;", new MySqlServerVersion(new Version(9, 0, 1)));
            // optionsBuilder.UseMySql("Server=db;Database=dbSeminar;Uid=kro;Pwd=ex;", new MySqlServerVersion(new Version(9, 0, 1))).UseLazyLoadingProxies();
            // optionsBuilder.UseMySql("server=localhost;user=kro;password=ex;Database=Seminar1;", new MySqlServerVersion(new Version(9, 0, 1)));
            // optionsBuilder.UseMySql("server=localhost;user=kro;password=ex;Database=dbSeminar;", new MySqlServerVersion(new Version(9, 0, 1))).UseLazyLoadingProxies();

            // ниже будет полный лог выдаваемой ошибки:
            /*
             *  PS C:\Users\tap-k\OneDrive\Рабочий стол\С#\Development of a network application\Seminar 5 DataBase\SeminarWork> dotnet ef database update
                Build started...
                Build succeeded.
                An error occurred using the connection to database '' on server 'db'.
                System.InvalidOperationException: An exception has been raised that is likely due to a transient failure. Consider enabling transient error resiliency by adding 'EnableRetryOnFailure()' to the 'UseMySql' call.
                 ---> MySqlConnector.MySqlException (0x80004005): Unable to connect to any of the specified MySQL hosts.
                   at MySqlConnector.Core.ServerSession.ConnectAsync(ConnectionSettings cs, MySqlConnection connection, Int64 startingTimestamp, ILoadBalancer loadBalancer, Activity activity, IOBehavior ioBehavior, CancellationToken cancellationToken) in /_/src/MySqlConnector/Core/ServerSession.cs:line 437
                   at MySqlConnector.MySqlConnection.CreateSessionAsync(ConnectionPool pool, Int64 startingTimestamp, Activity activity, Nullable`1 ioBehavior, CancellationToken cancellationToken) in /_/src/MySqlConnector/MySqlConnection.cs:line 932
                   at MySqlConnector.MySqlConnection.CreateSessionAsync(ConnectionPool pool, Int64 startingTimestamp, Activity activity, Nullable`1 ioBehavior, CancellationToken cancellationToken) in /_/src/MySqlConnector/MySqlConnection.cs:line 938
                   at MySqlConnector.MySqlConnection.OpenAsync(Nullable`1 ioBehavior, CancellationToken cancellationToken) in /_/src/MySqlConnector/MySqlConnection.cs:line 419
                   at MySqlConnector.MySqlConnection.Open() in /_/src/MySqlConnector/MySqlConnection.cs:line 381
                   at Microsoft.EntityFrameworkCore.Storage.RelationalConnection.OpenDbConnection(Boolean errorsExpected)
                   at Microsoft.EntityFrameworkCore.Storage.RelationalConnection.OpenInternal(Boolean errorsExpected)
                   at Microsoft.EntityFrameworkCore.Storage.RelationalConnection.Open(Boolean errorsExpected)
                   at Pomelo.EntityFrameworkCore.MySql.Storage.Internal.MySqlRelationalConnection.Open(Boolean errorsExpected)
                   at Pomelo.EntityFrameworkCore.MySql.Storage.Internal.MySqlDatabaseCreator.<>c__DisplayClass18_0.<Exists>b__0(DateTime giveUp)
                   at Microsoft.EntityFrameworkCore.ExecutionStrategyExtensions.<>c__DisplayClass12_0`2.<Execute>b__0(DbContext _, TState s)
                   at Pomelo.EntityFrameworkCore.MySql.Storage.Internal.MySqlExecutionStrategy.Execute[TState,TResult](TState state, Func`3 operation, Func`3 verifySucceeded)
                   --- End of inner exception stack trace ---
                   at Pomelo.EntityFrameworkCore.MySql.Storage.Internal.MySqlExecutionStrategy.Execute[TState,TResult](TState state, Func`3 operation, Func`3 verifySucceeded)
                   at Microsoft.EntityFrameworkCore.ExecutionStrategyExtensions.Execute[TState,TResult](IExecutionStrategy strategy, TState state, Func`2 operation, Func`2 verifySucceeded)
                   at Microsoft.EntityFrameworkCore.ExecutionStrategyExtensions.Execute[TState,TResult](IExecutionStrategy strategy, TState state, Func`2 operation)
                   at Pomelo.EntityFrameworkCore.MySql.Storage.Internal.MySqlDatabaseCreator.Exists(Boolean retryOnNotExists)
                   at Pomelo.EntityFrameworkCore.MySql.Storage.Internal.MySqlDatabaseCreator.Exists()
                   at Microsoft.EntityFrameworkCore.Migrations.HistoryRepository.Exists()
                   at Microsoft.EntityFrameworkCore.Migrations.Internal.Migrator.Migrate(String targetMigration)
                   at Microsoft.EntityFrameworkCore.Design.Internal.MigrationsOperations.UpdateDatabase(String targetMigration, String connectionString, String contextType)
                   at Microsoft.EntityFrameworkCore.Design.OperationExecutor.UpdateDatabaseImpl(String targetMigration, String connectionString, String contextType)
                   at Microsoft.EntityFrameworkCore.Design.OperationExecutor.UpdateDatabase.<>c__DisplayClass0_0.<.ctor>b__0()
                   at Microsoft.EntityFrameworkCore.Design.OperationExecutor.OperationBase.Execute(Action action)
                An exception has been raised that is likely due to a transient failure. Consider enabling transient error resiliency by adding 'EnableRetryOnFailure()' to the 'UseMySql' call.
             * 
             */

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users"); // Название колонки?

                entity.HasKey(x => x.Id).HasName("user_pkey"); // Авто инкремент
                entity.HasIndex(x => x.FullName).IsUnique(); // Уникальность поля(имени)

                entity.Property(es => es.FullName).HasColumnName("FullName").HasMaxLength(255).IsRequired(); // Выводим пользователей.
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("messages");

                entity.HasKey(f => f.MessageId).HasName("messagePK");

                entity.Property(e => e.Text).HasColumnName("messageName");
                entity.Property(e => e.DateSend).HasColumnName("messageData");
                entity.Property(e => e.IsSent).HasColumnName("is_sent");
                entity.Property(e => e.MessageId).HasColumnName("id");

                entity.HasOne(x => x.UserTO).WithMany(m => m.messagesTo).HasForeignKey(x => x.UserTOId).HasConstraintName("messageToUserFK");
                entity.HasOne(x => x.UserFrom).WithMany(m => m.messagesFrom).HasForeignKey(x => x.UserFromId).HasConstraintName("messageFromUserFK");
            });
        }


    }
}
