using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Logistic.Data
{/*
    CREATE TABLE EventLog(  
    [ID] int identity primary key,  
    [EventID] int,  
    [LogLevel] nvarchar(50), 
    [Message] nvarchar(4000), 
    [CreatedTime] datetime2 
) */
  public  class EventLog
    {
        public int Id { get; set; }
        public int EventId { get; set; }

        [MaxLength(50)]
        public string LogLevel { get; set; }

        [MaxLength(500)]
        public string Message { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
