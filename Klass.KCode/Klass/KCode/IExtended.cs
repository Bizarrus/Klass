namespace Klass.KCode {
    public interface IExtended {
        Property GetType();

        object GetValue();

        void SetValue(object value);

        int[] GetPosition();

        void SetPosition(int[] position);
    }
}
