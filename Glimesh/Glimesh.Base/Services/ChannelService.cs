﻿using Glimesh.Base.Models.Channels;
using Glimesh.Base.Models.GraphQL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glimesh.Base.Services
{
    /// <summary>
    /// The APIs for Channel-based services.
    /// </summary>
    public class ChannelService : GlimeshServiceBase
    {
        /// <summary>
        /// Creates an instance of the ChannelService.
        /// </summary>
        /// <param name="connection">The Glimesh connection to use</param>
        public ChannelService(GlimeshConnection connection) : base(connection) { }

        /// <summary>
        /// Gets all of the available channels
        /// </summary>
        /// <returns>All available channels</returns>
        public async Task<IEnumerable<ChannelModel>> GetLiveChannels(int count = 1)
        {
            GraphQLEdgeArrayModel<ChannelModel> channels = await this.QueryAsync<GraphQLEdgeArrayModel<ChannelModel>>($"{{ channels(status: LIVE, first: {count}) {{ {ChannelModel.BasicFieldsWithStreamerEdges} }} }}", "channels");
            return channels?.Items;
        }

        /// <summary>
        /// Gets the channel with the specified id.
        /// </summary>
        /// <param name="id">The id of the channel</param>
        /// <returns>The category</returns>
        public async Task<ChannelModel> GetChannelByID(string id) { return await this.QueryAsync<ChannelModel>($"{{ channel(id: \"{id}\") {{ {ChannelModel.AllFields} }} }}", "channel"); }

        /// <summary>
        /// Gets the channel with the specified id.
        /// </summary>
        /// <param name="name">The name of the channel</param>
        /// <returns>The channel</returns>
        public async Task<ChannelModel> GetChannelByName(string name) { return await this.QueryAsync<ChannelModel>($"{{ channel(streamerUsername: \"{name}\") {{ {ChannelModel.AllFields} }} }}", "channel"); }
    }
}