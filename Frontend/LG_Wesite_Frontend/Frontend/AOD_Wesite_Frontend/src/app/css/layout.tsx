import type { Metadata } from "next";
import localFont from "next/font/local";
import "./css/globals.css";
import "./css/common.css";
import "./css/slider.css";
import "./css/partner.css";

import Header from "./,,/components/Header";
import Footer from "./../components/Footer";
import Policy from "./../components/Policy";

// const geistSans = localFont({
//   src: "./fonts/segoe-ui-bold.woff",
//   variable: "--font-geist-sans",
//   weight: "100 900",
// });
// const geistMono = localFont({
//   src: "./fonts/segoe-ui.woff",
//   variable: "--font-geist-mono",
//   weight: "100 900",
// });

export const metadata: Metadata = {
  title: "Effort Service",
  description: "Generated by create next app",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body
      >
        <Header />
        {children}
        <Footer />
        <Policy />
      </body>
    </html>
  );
}
