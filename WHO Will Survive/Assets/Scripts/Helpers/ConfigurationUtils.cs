using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using UnityEngine;

/// <summary>
/// Provides utility access to configuration data
/// </summary>
public static class ConfigurationUtils
{
	#region Fields

	static ConfigurationData configurationData;

	#endregion

	#region Properties

	/// <summary>
	/// Gets the number of seconds in a game
	/// </summary>
	public static int TotalGameSeconds
    {
		get { return configurationData.TotalGameSeconds; }
	}

    #endregion

	#region Public methods

	/// <summary>
	/// Initializes the configuration data by reading the data from an XML configuration file
	/// </summary>
	public static void Initialize()
	{
		// temporary to write initial configuration data xml file
//		ConfigurationData.SaveDefaultValues();

		// deserialize configuration data from file into internal object
		FileStream fs = null;
		try
        {
			fs = new FileStream("ConfigurationData.xml",
				FileMode.Open);
			XmlDictionaryReader reader =
				XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
			DataContractSerializer ser = new DataContractSerializer(typeof(ConfigurationData));
			configurationData = (ConfigurationData)ser.ReadObject(reader, true);
			reader.Close();
		}
        finally
        {
			// always close input file
			if (fs != null)
            {
				fs.Close();
			}
		}
	}
	#endregion
}
