/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}" 
  ],
  theme: {
    extend: {
      colors: {
        teal: {
          500: '#38b2ac',
        },
        orange: {
          500: '#ed8936',
        },
        green: {
          500: '#48bb78',
        },
        blue: {
          500: '#4299e1',
        },
      },
    },
  },
  plugins: [],
}
