Cipher Toolkit

A C# console program that encrypts and decrypts messages with Caesar, Vigenère, and substitution ciphers. It can also count letter frequencies and try to break Caesar and Vigenère ciphers. This is my final project for CSE 210.

## How to use it

Pick an option from the menu:

1. Encrypt a message
2. Decrypt a message
3. Analyze letter frequencies
4. Try to break a cipher
5. Quit

When you encrypt or decrypt, you choose a cipher and enter its key:

- Caesar: a whole number
- Vigenere: a keyword
- Substitution: a 26-letter alphabet using each letter once (press Enter for the default)

## Things to know

- Breaking Vigenère needs a few hundred letters of text to work well.
- Breaking substitution ciphers is not supported.
- Only the letters A-Z are changed. Spaces and punctuation stay the same.

## Classes

- `Program`: starts the program
- `Menu`: handles all user input and output
- `Cipher`: abstract base class for the ciphers
- `CaesarCipher`, `VigenereCipher`, `SubstitutionCipher`: the three ciphers
- `Message`: holds text and whether it is encrypted
- `FrequencyAnalyzer`: counts letters and compares them to English
- `CodeBreaker`: guesses keys using the analyzer

## The four principles

- **Abstraction:** `Cipher` says what every cipher does (`Encrypt` and `Decrypt`) without saying how.
- **Encapsulation:** Keys and frequency tables are private. Other classes use public methods.
- **Inheritance:** The three ciphers inherit from `Cipher` and share its `Rotate` method and name.
- **Polymorphism:** `Menu` calls `Encrypt` and `Decrypt` on a `Cipher`, and each cipher runs its own version.