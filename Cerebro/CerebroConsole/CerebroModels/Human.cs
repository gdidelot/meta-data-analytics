using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerebroModels
{
    public class Human
    {
        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>
        /// The age.
        /// </value>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public Address Address { get; set; }

        /// <summary>
        /// Gets or sets the job.
        /// </summary>
        /// <value>
        /// The job.
        /// </value>
        public string Job { get; set; }

        /// <summary>
        /// Gets or sets the study.
        /// </summary>
        /// <value>
        /// The study.
        /// </value>
        public string Study { get; set; }

        /// <summary>
        /// Gets or sets the ethnicity.
        /// </summary>
        /// <value>
        /// The ethnicity.
        /// </value>
        public string Ethnicity { get; set; }

        /// <summary>
        /// Gets or sets the political mind.
        /// </summary>
        /// <value>
        /// The political mind.
        /// </value>
        public PoliticalMind PoliticalMind { get; set; }
    }
}
