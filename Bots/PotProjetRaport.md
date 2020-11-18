# Głosowy asystent inteligentnego domu

## Wstęp

Celem projektu było stworzenie bota wykonującego polecenia głosowe dotyczące systemu inteligentnego domu. 
Odpowiedzi równierz mają być udzielane głosowo.
W początkowej wersji najważniejsza jest możliwość odpytania o statusy zasilania urządzeń oraz zmiana tych statusów (włączenie/wyłączenie).

## Wykorzystane usługi

Do budowy bota wykorzystano usługi udostępniane przez platformę MS Azure.

Do zamiany mowy na tekst wykorzystano Transcribe speech input to text. 
Warto zauważyć, że  to rozwiązanie umożliwia nam łatwe przejście na przetwarzanie komend w różnych językach, ponieważ Translate speech with the speech service należy do tego samego pakietu usług.
Obie te usługi obsługuje się wykorzystując to samo SDK o bardzo zbliżonym interfejsie

Do zamiany tekstu na mowę wykorzystano Synthesize Text Input to Speech, również pochodzącą z tego samego pakietu, co dwie wspomniane wcześniej usługi. Dla wybranych języków udostępniane są tzw. głosy neuronowe, które brzmią bardziej naturalnie, ale też korzystanie z nich jest droższe.

Za zrozumienie intencji użytkownika odpowiada usługa LUIS.
Dzięki wykorzystaniu intencji typu Machine Learned nie są wymagane ściśle określone komendy, ponieważ Luis potrafi zaklasyfikować również komendy wypowiedziane innymi słowami, a nie dokładnie tymi, które zostały przewidziane i dodane do słowniczka.

## Tworzenie

Proces tworzenia opierał się głównie na programowaniu w języku C#. 
Poza tym należało też stworzyć odpowiednie usługi na Azure.
Istotną częścią pracy było również wypełnienie bazy wiedzy usługi LUIS.

## Architektura

Bot został zaprogramowany w języku C# z wykorzystaniem dedykowanych SDK do usług wymienionych powyżej.
Głównym celem było wytworzenie działającego modułu przetwarzania komend, więc interfejs aplikacji posiada minimum wymagane do testów.
Możliwe jest wydawanie komend po naciśnięciu przycisku oraz po każdej komendzie widoczna jest lista urządzeń "wirtualnego inteligentnego domu" i ich statusy.
Lista ta została zmockowana i jest generowana przy uruchamianiu aplikacji.

W aktualnej wersji użytkownik ma możliwość odpytywania o listę urządzeń z możliwością filtrowania po typie urządzenia, lokalizacji lub statusie włączony/wyłączony.
Można również włączyć lub wyłączyć urządzenie komendą głosową. 
Aktualnie wybór urządzenia odbywa się przez podanie typu i lokalizacji.
Jeśli w pierwotnej komendzie brakowało którejś z tych informacji, to bot o nie dopyta.
Możliwe jest przerwanie dialogu (również dopytywania) np. komendą "Cancel"
Poza tym dostępne są jeszcze komendy: "Who is at home?", "Get weather", "Check internet speed".
Obsługa nowych komend wymaga zmian w kodzie, jednak aby dodać lub usunąć typy urządzeń lub lokalizacje (np. kuchnia, biurko) nie jest to konieczne. 
Należy w tym celu jedynie dodać encję-dziecko dla odpowiedniej encji głównej.
Aplikacja przy starcie pobiera encje wykorzystując LUIS Authoring API i SDK oraz zapisuje je w bazie danych.

Kontekst bazy danych stworzono z wykorzystaniem EntityFrameworkCore, co umożliwia wygenerowanie bazy (podejście Code First) na różne systemy zarządzania. 
W celach testowych korzystano z bazy InMemory Sqlite.
Wspomniany moduł przetwarzania komend ma postać biblioteki .NET standard, co umożliwia wykorzystanie go w aplikacjach .NET Framework, .NET Core, a także Xamarin na urządzenia mobilne.

## Podsumowanie i napotkane problemy

Dzięki wykorzystaniu SDK wykorzystywanych usług tworzenie było znacznie szybsze i prostsze niż "standardowa" obsługa API HTTP.
Niestety mimo stosunkowo krótkiej i nieskomplikowanej  interakcji z tą blbloioteką natrafiłem na jej niedoskonałości w postaci klas obiektów nie zmapowanych w pełni.
Przez to konieczne było operowanie na JSON'ach co jest znacznie mniej wyygodne i czytelne niż gdyby obiekt był w pełni zmapowany na klaast .NET.

## Linki

* Krótka wideo-prezentacja bota: https://youtu.be/2A3grKJZCVQ
* Kod źródłowy rozwiązania: https://github.com/mati9725/AI-on-Microsoft-Azure-2020/tree/main/SmartHomeVoiceAssistant/SmartHomeBotWpfApp
* Backup LUISa: https://github.com/mati9725/AI-on-Microsoft-Azure-2020/tree/main/SmartHomeVoiceAssistant/LUIS_Backups