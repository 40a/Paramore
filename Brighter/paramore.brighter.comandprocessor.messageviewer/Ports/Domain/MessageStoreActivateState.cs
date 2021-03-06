// ***********************************************************************
// Assembly         : paramore.brighter.commandprocessor
// Author           : ian
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

using System;
using paramore.brighter.commandprocessor.messagestore.mssql;
using paramore.brighter.commandprocessor.messageviewer.Adaptors.API.Configuration.ConfigurationSections;

namespace paramore.brighter.commandprocessor.messageviewer.Ports.Domain
{
    public class MessageStoreActivationState 
    {
        internal MessageStoreActivationState(MessageViewerStoresElement messageStore)
            : this(messageStore.Name, messageStore.Type, messageStore.ConnectionString, messageStore.TableName)
        {
        }

        internal MessageStoreActivationState(string name, string type, string connectionString, string tableName)
            : this(name, type, connectionString)
        {
            TableName = tableName;
        }

        internal MessageStoreActivationState(string name, string type, string connectionString)
        {
            Name = name;
            TypeName = type;
            ConnectionString = connectionString;
            StoreType = MessageStoreTypeMapper.Map(TypeName, ConnectionString);
        }

        public MessageStoreType StoreType { get; private set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string ConnectionString { get; set; }
        public string TableName { get; set; }
    }

    public class MessageStoreTypeMapper
    {
        public static MessageStoreType Map(string typeName, string connectionString)
        {
            if (typeName == typeof(MsSqlMessageStore).FullName)
            {
                return connectionString.Contains("Server") ? MessageStoreType.SqlServer : MessageStoreType.SqlCe;
            }
            throw  new ArgumentException(string.Format("Do not recognise Messsage store type:{0} connection string:{1}", typeName, connectionString));
        }
    }
}