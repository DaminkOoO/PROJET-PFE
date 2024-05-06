using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using backend.Response;
using Hubs;
using Microsoft.AspNetCore.SignalR;

namespace backend.Service
{
    public class VoyageService
    {

        private List<Voyage> voyages = new List<Voyage>();
        private IHubContext<LocalisationHub> _localisationHub;
        public VoyageService(IHubContext<LocalisationHub> hub)
        {
            _localisationHub = hub;
        }
        public async ServiceResponse<> UpdataLocalisation (LocalisationDTO localisation){
            await _localisationHub.Clients.All.SendAsync("ReceiveLocalisation", localisation);

        }
        public void Enqueue(Voyage voyage)
        {
            voyages.Add(voyage);
            voyages.Sort((x, y) => x.Priorite.CompareTo(y.Priorite));
        }

        public Voyage Dequeue()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Queue is empty");
            }

            Voyage voyage = voyages[0];
            voyages.RemoveAt(0);
            return voyage;
        }

        public void ChangePriorite(string voyageId, int newPriorite)
        {
            Voyage voyage = voyages.Find(v => v.IdVoyage == voyageId)!;
            if (voyage != null)
            {
                voyage.Priorite = newPriorite;
                voyages.Sort((x, y) => x.Priorite.CompareTo(y.Priorite));
            }
            else
            {
                throw new KeyNotFoundException("Voyage not found");
            }
        }

        public bool IsEmpty()
        {
            return voyages.Count == 0;
        }


        public decimal CalculateDistance(decimal lat1, decimal lon1, decimal lat2, decimal lon2)
        {
            const decimal R = 6371;

            decimal dLat = (lat2 - lat1) * (decimal)Math.PI / 180;
            decimal dLon = (lon2 - lon1) * (decimal)Math.PI / 180;

            decimal a = (decimal)Math.Sin((double)(dLat / 2)) * (decimal)Math.Sin((double)(dLat / 2)) +
                       (decimal)Math.Cos((double)(lat1 * (decimal)Math.PI / 180)) * (decimal)Math.Cos((double)(lat2 * (decimal)Math.PI / 180)) *
                       (decimal)Math.Sin((double)(dLon / 2)) * (decimal)Math.Sin((double)(dLon / 2));

            decimal c = 2 * (decimal)Math.Atan2((double)Math.Sqrt((double)a), (double)Math.Sqrt((double)(1 - a)));

            decimal distance = R * c;
            return distance;
        }


    }
}