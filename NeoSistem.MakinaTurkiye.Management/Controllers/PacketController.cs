using NeoSistem.MakinaTurkiye.Management.Models.Entities;
using MakinaTurkiye.Services.Packets;
namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using EnterpriseEntity.Extensions.Data;
    using NeoSistem.MakinaTurkiye.Management.Models;
    using NeoSistem.MakinaTurkiye.Management.Models.ViewModel;
    using System;
    using System.Linq;
    using System.Web.Mvc;


    public class PacketController : BaseController
    {

        private readonly IPacketService _packetService;

        public PacketController(IPacketService packetService)
        {
            _packetService = packetService;
        }

        public enum PacketFeatureType
        {
            ProcessCount = 1,
            Active = 2,
            Content = 3
        }

        
        public ActionResult ViewPackets()
        {
            var model = new PacketViewModel();
           model.PacketFeatureItems = _packetService.GetAllPacketFeatures();
           model.PacketFeatureTypeItems = _packetService.GetAllPacketFeatureTypes().ToList();
            model.PacketItems = _packetService.GetPacketIsOnsetFalseByDiscountType(false).Where(x => x.DopingPacketDay.HasValue == false).ToList();
            return View(model);
        }

        public ActionResult Index()
        {
            PAGEID = PermissionPage.PaketListesi;

            var pItems = entities.Packets.ToList();
            return View(pItems);
        }

        public ActionResult Create()
        {
            PAGEID = PermissionPage.PaketEkle;

            return View(new PacketModel());
        }

        public ActionResult Edit(int id)
        {
            PAGEID = PermissionPage.PaketDuzenle;

            var model = new PacketModel();
            model.PacketFeatureItems = entities.PacketFeatures.Where(c => c.PacketId == id).ToList();

            var packet = entities.Packets.SingleOrDefault(c => c.PacketId == id);
            model.PacketName = packet.PacketName;
            model.PacketDay = packet.PacketDay;
            model.PacketDescription = packet.PacketDescription;
            model.PacketPrice = packet.PacketPrice;
            model.PacketColor = packet.PacketColor;
            model.IsStandart = packet.IsStandart.Value;
            model.IsOnset = packet.IsOnset.Value;
            model.Registered = packet.Registered.Value;
            model.UnRegistered = packet.UnRegistered.Value;
            model.SendReminderMail = packet.SendReminderMail;
            model.IsDiscounted = packet.IsDiscounted;
            model.HeaderColor = packet.HeaderColor;
            model.IsDopingPacket = packet.IsDopingPacket.HasValue == true ? packet.IsDopingPacket.Value : false;
            model.DopingPacketDay = packet.DopingPacketDay;
            model.ShowAdmin = packet.ShowAdmin.HasValue ? packet.ShowAdmin.Value : false;
            model.IsTryPacket = packet.IsTryPacket.HasValue ? packet.IsTryPacket.Value : false;
            model.ShowSetProcess = packet.ShowSetProcess.HasValue ? packet.ShowSetProcess.Value : false;


            if (packet.ProductFactor != null)
                model.ProductFactor = (float)packet.ProductFactor;

            if (packet.PacketOrder.HasValue)
            {
                model.PacketOrder = packet.PacketOrder.Value;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(string[] PacketFeature, PacketModel model, int id)
        {
            if (model.IsStandart)
            {
                var IsStandartPacketItems = entities.Packets.Where(c => c.IsStandart.Value);
                foreach (var item in IsStandartPacketItems)
                {
                    item.IsStandart = false;
                }
                entities.SaveChanges();
            }

            //if (model.IsOnset)
            //{
            //  var IsOnsetItems = entities.Packets.Where(c => c.IsOnset.Value);
            //  foreach (var item in IsOnsetItems)
            //  {
            //    item.IsOnset = false;
            //  }
            //  entities.SaveChanges();
            //}

            var packetFeatureItems = entities.PacketFeatures.Where(c => c.PacketId == id);
            foreach (var item in packetFeatureItems)
            {
                entities.PacketFeatures.DeleteObject(item);
            }
            entities.SaveChanges();

            var packet = entities.Packets.SingleOrDefault(c => c.PacketId == id);
            packet.PacketDay = model.PacketDay.HasValue ? model.PacketDay.Value : 0;
            packet.PacketDescription = model.PacketDescription;
            packet.PacketName = model.PacketName;
            packet.PacketPrice = model.PacketPrice;
            packet.PacketOrder = model.PacketOrder;
            packet.PacketColor = model.PacketColor;
            packet.IsStandart = model.IsStandart;
            packet.IsOnset = model.IsOnset;
            packet.Registered = model.Registered;
            packet.UnRegistered = model.UnRegistered;
            packet.SendReminderMail = model.SendReminderMail;
            packet.IsDiscounted = model.IsDiscounted;
            packet.HeaderColor = model.HeaderColor;
            packet.ProductFactor = model.ProductFactor;
            packet.IsDopingPacket = model.IsDopingPacket;
            packet.DopingPacketDay = model.DopingPacketDay;
            packet.ShowAdmin = model.ShowAdmin;
            packet.IsTryPacket = model.IsTryPacket;
            packet.ShowSetProcess = model.ShowSetProcess;
            entities.SaveChanges();

            int packetId = id;
            if (model.Registered == false && model.UnRegistered == false)
            {
                foreach (var item in PacketFeature)
                {
                    var packetFeature = new PacketFeature
                    {
                        PacketId = packetId,
                        PacketFeatureTypeId = Convert.ToByte(item.Split(',').GetValue(0)),
                        FeatureType = Convert.ToByte(item.Split(',').GetValue(1)),
                    };

                    var featureType = (PacketFeatureType)Convert.ToByte(item.Split(',').GetValue(1));
                    switch (featureType)
                    {
                        case PacketFeatureType.ProcessCount:
                            packetFeature.FeatureProcessCount = item.Split(',').GetValue(2).ToString() == "" ? Convert.ToByte(0) : Convert.ToByte(item.Split(',').GetValue(2));
                            break;
                        case PacketFeatureType.Active:
                            packetFeature.FeatureActive = Convert.ToBoolean(item.Split(',').GetValue(2));
                            break;
                        case PacketFeatureType.Content:
                            packetFeature.FeatureContent = Convert.ToString(item.Split(',').GetValue(2));
                            break;
                        default:
                            break;
                    }

                    entities.PacketFeatures.AddObject(packetFeature);
                    entities.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(string[] PacketFeature, PacketModel model)
        {
            if (model.IsStandart)
            {
                var IsStandartPacketItems = entities.Packets.Where(c => c.IsStandart.Value);
                foreach (var item in IsStandartPacketItems)
                {
                    item.IsStandart = false;
                }
                entities.SaveChanges();
            }
            //if (model.IsOnset)
            //{
            //  var IsOnsetItems = entities.Packets.Where(c => c.IsOnset.Value);
            //  foreach (var item in IsOnsetItems)
            //  {
            //    item.IsOnset = false;
            //  }
            //  entities.SaveChanges();
            //}

            var packet = new Packet
            {
                PacketDay = model.PacketDay.HasValue ? model.PacketDay.Value : 0,
                PacketDescription = model.PacketDescription,
                PacketName = model.PacketName,
                PacketPrice = model.PacketPrice,
                PacketOrder = model.PacketOrder,
                PacketColor = model.PacketColor,
                IsStandart = model.IsStandart,
                IsOnset = model.IsOnset,
                Registered = model.Registered,
                UnRegistered = model.UnRegistered,
                SendReminderMail = model.SendReminderMail,
                IsDiscounted = model.IsDiscounted,
                HeaderColor = model.HeaderColor,
                ProductFactor = model.ProductFactor,
                DopingPacketDay = model.DopingPacketDay,
                IsDopingPacket = model.IsDopingPacket,
                ShowAdmin = model.ShowAdmin,
                IsTryPacket = model.IsTryPacket
            };
            entities.Packets.AddObject(packet);
            entities.SaveChanges();

            int packetId = packet.PacketId;
            if (model.Registered == false && model.UnRegistered == false)
            {
                foreach (var item in PacketFeature)
                {
                    var packetFeature = new PacketFeature
                    {
                        PacketId = packetId,
                        PacketFeatureTypeId = Convert.ToByte(item.Split(',').GetValue(0)),
                        FeatureType = Convert.ToByte(item.Split(',').GetValue(1)),
                    };

                    var featureType = (PacketFeatureType)Convert.ToByte(item.Split(',').GetValue(1));
                    switch (featureType)
                    {
                        case PacketFeatureType.ProcessCount:
                            packetFeature.FeatureProcessCount = item.Split(',').GetValue(2).ToString() == "" ? Convert.ToByte(0) : Convert.ToByte(item.Split(',').GetValue(2));
                            break;
                        case PacketFeatureType.Active:
                            packetFeature.FeatureActive = Convert.ToBoolean(item.Split(',').GetValue(2));
                            break;
                        case PacketFeatureType.Content:
                            packetFeature.FeatureContent = Convert.ToString(item.Split(',').GetValue(2));
                            break;
                        default:
                            break;
                    }

                    entities.PacketFeatures.AddObject(packetFeature);
                    entities.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var curPacket = entities.Packets.SingleOrDefault(c => c.PacketId == id);
                entities.Packets.DeleteObject(curPacket);
                entities.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false);
                throw;
            }
            return Json(true);
        }

        public void PacketFull()
        {
            var packetFeatureType = entities.PacketFeatureTypes.ToList();
            foreach (var item in packetFeatureType)
            {
                var Packet = new PacketFeature();
                Packet.FeatureActive = true;
                Packet.PacketId = 31;
                Packet.PacketFeatureTypeId = item.PacketFeatureTypeId;
                Packet.FeatureType = (byte)2;
                entities.PacketFeatures.AddObject(Packet);
                entities.SaveChanges();
            }
        }
    }
}
