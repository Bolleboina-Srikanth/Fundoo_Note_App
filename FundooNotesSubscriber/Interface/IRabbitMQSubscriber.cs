using System;
using System.Collections.Generic;
using System.Text;

namespace FundooNotesSubscriber.Interface
{
    public interface IRabbitMQSubscriber
    {
        void ConsumeMessages();
    }
}
