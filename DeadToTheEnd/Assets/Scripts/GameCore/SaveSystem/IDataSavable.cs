namespace GameCore.SaveSystem
{
    public interface IDataSavable
    {
        ISerializable SerializableData();
        void RestoreSerializableData(ISerializable serializable);
    }
}