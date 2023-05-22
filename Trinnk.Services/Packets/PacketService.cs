using Trinnk.Caching;
using Trinnk.Core.Data;
using Trinnk.Entities.Tables.Packets;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Trinnk.Services.Packets
{
    public class PacketService : BaseService, IPacketService
    {
        #region Fields

        private readonly IRepository<Packet> _packetRepository;
        private readonly IRepository<PacketFeature> _packetFeatureRepository;
        private readonly IRepository<PacketFeatureType> _packetFeatureTypeRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public PacketService(IRepository<Packet> packetRespository,
            IRepository<PacketFeatureType> packetFeatureTypeRepository,
            IRepository<PacketFeature> packetFeatureRepository, ICacheManager cacheManager) : base(cacheManager)
        {
            this._packetRepository = packetRespository;
            this._packetFeatureTypeRepository = packetFeatureTypeRepository;
            this._packetFeatureRepository = packetFeatureRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public Packet GetPacketByPacketId(int packetId)
        {
            if (packetId == 0)
                throw new ArgumentException("packetId");

            var query = _packetRepository.Table;
            query = query.Include(p => p.PacketFeatures);

            var packet = query.FirstOrDefault(p => p.PacketId == packetId);
            return packet;
        }

        public Packet GetPacketByIsStandart(bool isStandart)
        {
            var query = _packetRepository.Table;

            var packet = query.FirstOrDefault(p => p.IsStandart == isStandart);
            return packet;
        }


        public PacketFeatureType GetPacketFeatureTypeByPacketFeatureTypeId(int packetFeatureTypeId)
        {
            if (packetFeatureTypeId == 0)
                throw new ArgumentException("packetFeatureTypeId");

            var query = _packetFeatureTypeRepository.Table;
            return query.FirstOrDefault(p => p.PacketFeatureTypeId == packetFeatureTypeId);

        }

        public IList<PacketFeature> GetAllPacketFeatures()
        {
            var showPacketFeatureTypeItems = new List<int>();


            var query = _packetFeatureRepository.Table;
            query.Include(x => x.PacketFeatureType);
            return query.ToList();
        }

        public IList<Packet> GetPacketIsOnsetFalseByDiscountType(bool isDiscounted)
        {
            var query = _packetRepository.Table;
            return query.Where(p => p.IsDiscounted == isDiscounted && p.IsOnset == false).OrderBy(x => x.PacketOrder).ToList();
        }

        public IList<Packet> GetAllPacket()
        {
            var query = _packetRepository.Table;
            return query.OrderByDescending(p => p.PacketOrder).ToList();
        }

        public PacketFeature GetPacketFeatureByPacketIdAndPacketFeatureTypeId(int packetId, int packetFeatureTypeId)
        {
            if (packetId == 0 || packetFeatureTypeId == 0)
                throw new ArgumentException("packetId or packetFeatureTypeId");

            var query = _packetFeatureRepository.Table;
            return query.FirstOrDefault(p => p.PacketId == packetId && p.PacketFeatureTypeId == packetFeatureTypeId);
        }

        public IList<PacketFeatureType> GetAllPacketFeatureTypes()
        {
            var query = _packetFeatureTypeRepository.Table;
            var packetFeatureTypes = query.ToList();
            return packetFeatureTypes;
        }


        #endregion

    }
}
