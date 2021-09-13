using DataServer.Data.DataLoaders;
using DataServer.Models;
using HotChocolate;
using HotChocolate.Types;
using System.Threading;
using System.Threading.Tasks;

namespace DataServer.Data.Subscriptions
{
    /// <summary>
    /// 会议订阅
    /// </summary>
    [ExtendObjectType(OperationTypeNames.Subscription)]
    public class SessionSubscriptions
    {
        /// <summary>
        /// 当会议被调度时触发
        /// 同时也是自动订阅的订阅名
        /// </summary>
        /// <param name="sessionId">会议Id，订阅的事件消息</param>
        /// <param name="sessionById">数据加载器</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>订阅将要收到的消息负载</returns>
        // 使用Subscribe特性告诉框架把方法配置为一个订阅
        [Subscribe]
        // 使用Topic特性告诉框架自动推断订阅和主题
        [Topic]
        public Task<Session> OnSessionScheduledAsync(
            // 使用EventMessage特性告诉框架该参数是订阅的事件消息
            [EventMessage] int sessionId,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken) =>
                sessionById.LoadAsync(sessionId, cancellationToken);
    }
}
