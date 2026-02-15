
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization;

namespace AuraShop.Shared.Mongo
{
   
    public class LowerCaseElementNameConvention : IMemberMapConvention
    {
        public string Name => "LowerCaseElementNameConvention";
        public void Apply(BsonMemberMap memberMap) => memberMap.SetElementName(memberMap.MemberName.ToLowerInvariant());
    }

    public class MongoConvention
    {
        public static void AddMongoConventionPack()
        {
            var pack = new ConventionPack {
                new LowerCaseElementNameConvention(),
                new IgnoreIfNullConvention(true),
                new IgnoreExtraElementsConvention(true)
            };

            ConventionRegistry.Register("LowerCase", pack, t => true);
        }
    }
}
