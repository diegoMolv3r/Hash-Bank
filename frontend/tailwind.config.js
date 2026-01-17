/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  darkMode: 'class',
  theme: {
    extend: {
      colors: {
        "primary": "#2bee8c",
        "background-light": "#f6f8f7",
        "background-dark": "#102219",
        "hash-blue": "#0d1b14",
      },
      fontFamily: {
        "display": ["Manrope", "sans-serif"]
      }
    },
  },
  plugins: [],
}
