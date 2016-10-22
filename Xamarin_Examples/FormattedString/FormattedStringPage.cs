using System;


using Xamarin.Forms;

namespace Greetings
{
    public class FormattedStringPage : ContentPage
    {
        public FormattedStringPage() {
            FormattedString formattedString = new FormattedString {
               Spans = {
                   new Span {
                        Text = "\u201CXamarin\u201D",
                        FontSize = Device.GetNamedSize( NamedSize.Large, typeof( Label ) ),
                        FontAttributes = FontAttributes.Bold | FontAttributes.Italic
                    },
                    new Span {
                        Text = ", how do I say this word",
                        FontSize = Device.GetNamedSize( NamedSize.Micro, typeof( Label ) )
                    },
                    new Span {
                        Text = "?",
                        FontSize = Device.GetNamedSize( NamedSize.Small, typeof( Label ) ),
                        FontAttributes = FontAttributes.Bold
                    }
                }
            };

            Content = new Label {
                FormattedText = formattedString,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Black,
                TextColor = Color.Lime
            };

        }
    }
}
