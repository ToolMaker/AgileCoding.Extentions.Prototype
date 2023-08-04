namespace AgileCoding.Extentions.Prototypes.Serialization
{
    using AgileCoding.Extentions.Generics;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security;
    using System.Xml.Serialization;

    public static class PrototypeExtentions
    {
        /// <summary>
        /// Using DeepBinary Copy requires that all types used in the copy are Serializable.
        /// Uses a memory stream to serialize.
        /// This is the fastest serialization availble currently.
        /// </summary>
        /// <typeparam name="TypeToCopy">Generic type to copy</typeparam>
        /// <param name="self">the object the extention is being called on</param>
        /// <returns>The copied object</returns>
        [Obsolete]
        public static TypeToCopy DeepBinaryCopy<TypeToCopy>(this TypeToCopy self)
        {
            self.ThrowIfNull<TypeToCopy, ArgumentNullException>($"{nameof(DeepBinaryCopy)} extention method had a {nameof(ArgumentNullException)}. It seems the object were {nameof(DeepBinaryCopy)} method is called on is null");

            using (var memoryStream = new MemoryStream())
            {
                var binaryFormattter = new BinaryFormatter();
                try
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    binaryFormattter.Serialize(memoryStream, self);
#pragma warning restore CS8604 // Possible null reference argument.
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return (TypeToCopy)binaryFormattter.Deserialize(memoryStream);
                }
                catch (SerializationException se)
                {
                    throw new SerializationException($"{nameof(DeepBinaryCopy)} extention method had a serialization exception. {nameof(DeepBinaryCopy)} requires that all types are marked serializable. If this is not posisble you can explore other Deep copy operations", se);
                }
                catch (SecurityException se)
                {
                    throw new SecurityException($"{nameof(DeepBinaryCopy)} extention method had a security exception.", se);
                }
                catch (ArgumentNullException ane)
                {
                    throw new ArgumentNullException($"{nameof(DeepBinaryCopy)} extention method had a {nameof(ArgumentNullException)}. It seems the object were {nameof(DeepBinaryCopy)} method is called on is null", ane);
                }
            }
        }

        /// <summary>
        /// Using DeepXmlCopy requires that all types has a parameterless constructor.
        /// Uses a memory stream to serialize.
        /// DeepJSONNewtonsoftCopy is faster than DeepXMLCopy
        /// </summary>
        /// <typeparam name="TypeToCopy">Generic type to copy</typeparam>
        /// <param name="self">the object the extention is being called on</param>
        /// <returns>The copied object</returns>
        public static TypeToCopy DeepXMLCopy<TypeToCopy>(this TypeToCopy self)
            where TypeToCopy : class
        {
            self.ThrowIfNull<TypeToCopy, ArgumentNullException>($"{nameof(DeepXMLCopy)} extention method had a {nameof(ArgumentNullException)}. It seems the object were {nameof(DeepXMLCopy)} method is called on is null");

            using (var memoryStream = new MemoryStream())
            {
                var xmlSerializer = new XmlSerializer(typeof(TypeToCopy));
                try
                {
                    xmlSerializer.Serialize(memoryStream, self);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var copyType = xmlSerializer.Deserialize(memoryStream) as TypeToCopy;
                    if (copyType == null)
                    {
                        throw new Exception($"{nameof(DeepXMLCopy)} extention method had a {nameof(Exception)}. Note: {nameof(DeepXMLCopy)} requires all types to have a paremeterless constructor.");
                    }
                    return copyType;
                }
                catch (Exception e)
                {
                    throw new Exception($"{nameof(DeepXMLCopy)} extention method had a {nameof(Exception)}. Note: {nameof(DeepXMLCopy)} requires all types to have a paremeterless constructor.", e);
                }
            }
        }

        /// <summary>
        /// Using DeepJSONNewtonsoftCopy requires that all types has a parameterless constructor.
        /// Uses a memory stream to serialize.
        /// DeepJSONNewtonsoftCopy is faster than DeepXMLCopy
        /// </summary>
        /// <typeparam name="TypeToCopy">Generic type to copy</typeparam>
        /// <param name="self">the object the extention is being called on</param>
        /// <returns>The copied object</returns>
        public static TypeToCopy DeepJSONNewtonsoftCopy<TypeToCopy>(this TypeToCopy self)
        {
            self.ThrowIfNull<TypeToCopy, ArgumentNullException>($"{nameof(DeepJSONNewtonsoftCopy)} extention method had a {nameof(ArgumentNullException)}. It seems the object were {nameof(DeepJSONNewtonsoftCopy)} method is called on is null");

            var copyType =  JsonConvert.DeserializeObject<TypeToCopy>(JsonConvert.SerializeObject(self));
            if (copyType == null)
            {
                throw new Exception($"{nameof(DeepJSONNewtonsoftCopy)} extention method had a {nameof(Exception)}. Note: {nameof(DeepJSONNewtonsoftCopy)} requires all types to have a paremeterless constructor.");
            }
            return copyType;
        }
    }
}
