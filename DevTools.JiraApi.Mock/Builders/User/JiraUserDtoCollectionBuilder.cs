using DevTools.JiraApi.JiraDto;

namespace DevTools.JiraApi.Mock
{
    public class JiraUserDtoCollectionBuilder : CollectionBuilder<JiraUserDtoCollectionBuilder, JiraUserDto>
    {
        private JiraUserDtoCollectionBuilder()
        {}

        public static JiraUserDtoCollectionBuilder Empty()
            => new JiraUserDtoCollectionBuilder();

        public static JiraUserDtoCollectionBuilder FromNames(params string[] name)
            => Empty().For(name.Length, (i, b) => b.Add(JiraUserDtoBuilder.FromDisplayName(name[i]).Build()));
    }
}
