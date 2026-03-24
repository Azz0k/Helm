import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import * as path from "node:path";
import * as fs from "node:fs";

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server:{
      port:443,
      https:{
        pfx: fs.readFileSync(path.join(__dirname, 'cert_localhost.pfx')),
        passphrase: 'sample',
      }
  }
})
