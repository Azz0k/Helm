import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import fs from "node:fs";
import path from "node:path";
import tailwindcss from '@tailwindcss/vite';

// https://vite.dev/config/
export default defineConfig({
  plugins: [vue(), tailwindcss()],
  server:{
    port:443,
    https:{
      pfx: fs.readFileSync(path.join(__dirname, 'cert_localhost.pfx')),
      passphrase: 'sample',
    }
  },
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"),
    },
  },
})
