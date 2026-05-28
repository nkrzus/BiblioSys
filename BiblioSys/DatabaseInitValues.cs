
// Inicjalizacja wartości w bazie danych

// TABELA AUTHORS

//INSERT INTO Authors(FirstName, LastName) VALUES
//('Jan', 'Kowalski'),
//('Anna', 'Nowak'),
//('Piotr', 'Wiśniewski'),
//('Maria', 'Wójcik'),
//('Tomasz', 'Kaczmarek'),
//('Katarzyna', 'Mazur'),
//('Michał', 'Krawczyk'),
//('Agnieszka', 'Zielińska'),
//('Paweł', 'Szymański'),
//('Magdalena', 'Woźniak'),
//('Krzysztof', 'Dąbrowski'),
//('Ewa', 'Kozłowska'),
//('Adam', 'Jankowski'),
//('Joanna', 'Wojciechowska'),
//('Łukasz', 'Kwiatkowski'),
//('Monika', 'Kubiak'),
//('Mateusz', 'Piotrowski'),
//('Natalia', 'Grabowska'),
//('Jakub', 'Zawadzki'),
//('Karolina', 'Pawlak'),
//('Rafał', 'Michalski'),
//('Julia', 'Król'),
//('Dawid', 'Wieczorek'),
//('Oliwia', 'Jabłońska'),
//('Sebastian', 'Nowicki'),
//('Weronika', 'Majewska'),
//('Patryk', 'Ostrowski'),
//('Zuzanna', 'Sikora'),
//('Bartosz', 'Czarnecki'),
//('Emilia', 'Sawicka');

// TABELA BOOKS

//INSERT INTO Books(Title, Category, AuthorId, Description, IsFree) VALUES
//('Cień wspomnień', 0, 1, 'Poruszająca opowieść o miłości, która przetrwała lata rozłąki. Bohaterowie wracają do miejsc, które kiedyś ich połączyły. To historia pełna emocji i refleksji.', 0),
//('Granice wiedzy', 1, 2, 'Książka przedstawia fascynujące odkrycia naukowe ostatnich dekad. Autor w przystępny sposób tłumaczy skomplikowane zjawiska. Idealna dla ciekawych świata.', 1),
//('Życie bez maski', 2, 3, 'Biografia człowieka, który zmienił oblicze swojej branży. Szczera, pełna trudnych momentów i inspirujących decyzji. Pokazuje, jak wiele można osiągnąć dzięki determinacji.', 0),
//('Echo przeszłości', 3, 4, 'Historia rodziny uwikłanej w wydarzenia XX wieku. Każde pokolenie mierzy się z innymi wyzwaniami. To opowieść o pamięci, tożsamości i sile tradycji.', 0),
//('Droga mistrza', 4, 5, 'Opowieść o sportowcu, który pokonał własne słabości. Trening, poświęcenie i pasja prowadzą go na szczyt. Inspirująca historia dla każdego, kto walczy o swoje marzenia.', 1),
//('Sztuka milczenia', 5, 6, 'Autor analizuje znaczenie ciszy w kulturze i sztuce. Książka pełna jest przykładów z historii malarstwa i muzyki. To refleksyjna podróż przez świat artystycznej ekspresji.', 0),
//('Gwiezdny szlak', 6, 7, 'Futurystyczna opowieść o ludzkości wyruszającej poza granice galaktyki. Bohaterowie odkrywają nowe światy i mierzą się z nieznanym. Dynamiczna i pełna zwrotów akcji.', 0),
//('Serce w płomieniach', 0, 8, 'Romans o dwojgu ludzi, których los połączył w najmniej oczekiwanym momencie. Ich uczucie rozwija się mimo przeciwności. Ciepła i wzruszająca historia.', 1),
//('Kod natury', 1, 9, 'Autor opisuje ukryte mechanizmy rządzące światem biologii. Książka łączy naukę z fascynującymi przykładami z życia. Idealna dla pasjonatów przyrody.', 0),
//('Człowiek z cienia', 2, 10, 'Biografia tajemniczej postaci, która wpłynęła na losy wielu ludzi. Pełna nieznanych faktów i zaskakujących zwrotów. Odsłania kulisy życia w cieniu wielkich wydarzeń.', 0),
//('Kraina popiołu', 3, 11, 'Powieść historyczna osadzona w czasach wielkich przemian. Bohaterowie walczą o przetrwanie w trudnych realiach. To historia o odwadze i nadziei.', 1),
//('Siła charakteru', 4, 12, 'Książka o sportowcach, którzy zmienili oblicze swoich dyscyplin. Autor opisuje ich drogę do sukcesu. Inspirująca i motywująca.', 0),
//('Oczy artysty', 5, 13, 'Esej o tym, jak artyści postrzegają świat. Pełen przykładów z historii sztuki i współczesnych analiz. Zachęca do refleksji nad własną kreatywnością.', 0),
//('Nowy Eden', 6, 14, 'Science fiction o kolonizacji nowej planety. Ludzie próbują stworzyć idealne społeczeństwo, ale szybko pojawiają się konflikty. Wciągająca i pełna napięcia.', 1),
//('Miłość na rozdrożu', 0, 15, 'Historia o trudnych wyborach sercowych. Bohaterowie muszą zmierzyć się z konsekwencjami swoich decyzji. Pełna emocji i wzruszeń.', 0),
//('W imię nauki', 1, 16, 'Autor opisuje przełomowe eksperymenty, które zmieniły świat. Książka pokazuje kulisy pracy naukowców. Fascynująca i pouczająca.', 0),
//('Prawdziwe oblicze', 2, 17, 'Biografia osoby, która przez lata ukrywała swoją przeszłość. Odkrywa dramatyczne wydarzenia i trudne wybory. Wciągająca i pełna emocji.', 1),
//('Wojna pokoleń', 3, 18, 'Powieść historyczna o konflikcie między tradycją a nowoczesnością. Bohaterowie próbują odnaleźć swoje miejsce w zmieniającym się świecie. Głęboka i poruszająca.', 0),
//('Droga do zwycięstwa', 4, 19, 'Opowieść o sportowcu, który mimo przeciwności osiągnął sukces. Pokazuje siłę charakteru i determinacji. Inspirująca historia.', 0),
//('Barwy duszy', 5, 20, 'Książka o emocjach wyrażanych poprzez sztukę. Autor analizuje dzieła znanych twórców. To podróż przez świat artystycznych interpretacji.', 1),
//('Ostatnia misja', 6, 21, 'Science fiction o załodze statku kosmicznego wykonującej niebezpieczną misję. Napięcie rośnie z każdą stroną. Idealna dla fanów gatunku.', 0),
//('Złamane serca', 0, 22, 'Romans o miłości, która musi przetrwać próbę czasu. Bohaterowie uczą się wybaczać i walczyć o siebie. Ciepła i wzruszająca opowieść.', 0),
//('Granice logiki', 1, 23, 'Książka o największych zagadkach nauki. Autor przedstawia je w przystępny sposób. Inspiruje do myślenia.', 1),
//('Cień bohatera', 2, 24, 'Biografia człowieka, który poświęcił wszystko dla innych. Pełna dramatycznych momentów i wzruszeń. Pokazuje, czym jest prawdziwa odwaga.', 0),
//('Królestwo piasku', 3, 25, 'Powieść historyczna osadzona w starożytnym świecie. Bohaterowie walczą o władzę i przetrwanie. Pełna intryg i zwrotów akcji.', 0),
//('Siła ducha', 4, 26, 'Książka o sportowcach, którzy pokonali własne ograniczenia. Inspirująca i motywująca. Pokazuje, że wszystko jest możliwe.', 1),
//('Dotyk piękna', 5, 27, 'Esej o znaczeniu estetyki w życiu codziennym. Autor analizuje wpływ sztuki na emocje. Zachęca do refleksji.', 0),
//('Gwiezdny bunt', 6, 28, 'Science fiction o rebelii przeciwko tyranicznej władzy. Pełna akcji i dramatycznych momentów. Wciąga od pierwszej strony.', 0),
//('Świt uczuć', 0, 29, 'Romans o miłości, która rodzi się w najmniej oczekiwanym momencie. Bohaterowie odkrywają, co naprawdę jest ważne. Ciepła i pełna emocji.', 1),
//('Władcy przyszłości', 6, 30, 'Opowieść o świecie rządzonym przez technologię. Ludzie próbują odzyskać wolność w świecie pełnym kontroli. Dynamiczna i pełna napięcia.', 0);

// DODANIE ADMINA

//insert into users(FirstName, LastName, Email, AccountBalance, Password)
//values ('admin', 'admin', 'admin@admin.com',0,'admin')