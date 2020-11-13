# Create Intelligent Bots with the Azure Bot Service

## QnA Maker and Azure Bot Service

### 1. Wstęp

Usługa QnA Maker pozwala stworzyć bazę pytań i odpowiedzi. Moż ona stanowić bazę wiedzy dla bota obsługującego teksty w języku naturalnym. Połączenie wykorzystania QnA Maker i Azure Bot Service umożliwia zbudowanie bota i podłączenie go do różnych kanałów bez pisania jakiegokolwiek kodu. Kanałami tymi może być np. Facebook, MS Teams, email i wiele innych.

### 2. Przypadki użycia

* Odpowiadanie klientom na często zadawane pytania dotyczące oferowanych produktów
* Jeden bot może wspierać różne działy w firmie w odpowiadaniu na pytania od pracowników
* Bot wspierający rodziców zadających pytania o opiekę nad małym dzieckiem

### 3. Użytkowanie

Tworzenie bazy wiedzy w QnA Maker odbywa się przez dodawanie par pytanie-odpowiedź przez interfejs webowy lub importowanie ich z przygotowanych plików (np. .docx). Bazę wiedzy stworzoną w QnA Maker można wykorzystywać także bez Azure Bot Service. Realizuje się to przez wysyłanie zapytań HTTP.

Azure Bot Service poza mozliwością podłączania się do popularnych komunikatorów, udostępnia fragment kodu HTML, który można łatwo osadzić na stronie internetowej. Kod ten powoduje wyświetlenie okna czatu w którym można prowadzić dialog ze stworzonym botem.

**Cennik**

Użytkowanie Azure Bot service jest darmowe na kanałach standardowych (np. Skype, Slack), a na kanałach premnium (własna aplikacja webowa) w planie darmowym dostępne jest 10000 wiadomości miesięcznie. 
W planie płatnym koszt tysiąca wiadomości premium to 0,50$. 
Należy jednak pamiętać, że dodatkowo płatne mogą być usługi powiązane z botem jak np. hostowanie bota jako aplikacji webowej, Application Insights lub usługi z bazą wiedzy bota (LUIS, QnA Maker API). Usługi te są płatne według swoich cenników.

W planie standardowym można wykonywać maksymalnie 3 transakcje na sekundę i 100 na minutę. 
Plan darmowy poza wymienionymi limitami ogranicza liczbę transakcji do 50tys. miesięcznie i rozmiar pojedynczego dokumentu do 1MB. 
W planie darmowym można zaimportować tylko 3 dokumenty z bazą wiedzy. 
W standardowym nielimitowana ilość importów kosztuje 10$ miesięcznie.
Tak samo jak w przypadku Azure Bot Service dodatkowo płaci się za hostowanie (App Service) czy opcjonalne Application Insights.

## Bot Framework Composer 

### 1. Wstęp

Bot Framework Composer jest rozwiązaniem low-code'owym (lub nawet bezkodowym) służącym do tworzenia botów.
Umożliwia integrację z usługami takimi jak LUIS czy QnA Maker.
Dzięki przystępnemu interfejsowi bez problemu mogą z niego korzystać nie tylko programiści, ale też ludzie zajmujący się innymi dziedzinami.

### 2. Przypadki użycia

* Tworzenie botów przez osoby nie zajmujące się profesjonalnie programowaniem
* Szybkie (a przez to tańsze) stworzenie minimalnego produktu (MVP), który następnie można rozwijać o bardziej zaawansowane funkcje stosując kodowe rozwiązania
* Budowa wielotematycznych botów o różnorodnych ścieżkach rozmowy

### 3. Użytkowanie

Bot Framework Composera używa się jako aplikacji desktopowej dostępnej do pobrania za darmo. 
Jako oddzielna aplikacja dostępny jest też Bot Framework Emulator umożliwiający testowanie bota na naszym komputerze.

**Cennik**

Bot Framework Composer jest darmowym rozwiązaniem o otwartym źródle. 
Nalezy jednak pamiętać, że usługi z którymi integrujemy naszego bota (np. LUIS) mogą być płatne.