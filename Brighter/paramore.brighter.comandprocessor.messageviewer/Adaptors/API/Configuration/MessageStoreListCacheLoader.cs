// ***********************************************************************
// Assembly         : paramore.brighter.commandprocessor
// Author           : ianp
// Created          : 25-03-2014
//
// Last Modified By : ian
// Last Modified On : 25-03-2014
// ***********************************************************************
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

#region Licence
/* The MIT License (MIT)
Copyright � 2014 Ian Cooper <ian_hammond_cooper@yahoo.co.uk>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the �Software�), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED �AS IS�, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. */

#endregion

using paramore.brighter.commandprocessor.Logging;
using paramore.brighter.commandprocessor.messagestore.mssql;
using paramore.brighter.commandprocessor.messageviewer.Ports.Domain;

namespace paramore.brighter.commandprocessor.messageviewer.Adaptors.API.Configuration
{

    public interface IMessageStoreListCacheLoader
    {
        IMessageStoreActivationStateCache Load();
    }

    public class MessageStoreListCacheLoader : IMessageStoreListCacheLoader
    {
        private ILog _logger = LogProvider.GetLogger("MessageStoreListCacheLoader");
        private readonly IMessageStoreActivationStateCache _messageStoreActivationStateCache;

        public MessageStoreListCacheLoader(IMessageStoreActivationStateCache messageStoreActivationStateCache)
        {
            _messageStoreActivationStateCache= messageStoreActivationStateCache;
        }

        public IMessageStoreActivationStateCache Load()
        {
            _messageStoreActivationStateCache.Set(MessageStoreType.SqlServer,
                (storeConfig) => new MsSqlMessageStore(
                    new MsSqlMessageStoreConfiguration(storeConfig.ConnectionString, storeConfig.TableName,
                        MsSqlMessageStoreConfiguration.DatabaseType.MsSqlServer), _logger));

            _messageStoreActivationStateCache.Set(MessageStoreType.SqlCe,
                (storeConfig) => new MsSqlMessageStore(
                    new MsSqlMessageStoreConfiguration(storeConfig.ConnectionString, storeConfig.TableName,
                        MsSqlMessageStoreConfiguration.DatabaseType.SqlCe), _logger));
            return _messageStoreActivationStateCache;
        }
    }
}