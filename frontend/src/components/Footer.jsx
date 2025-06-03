import React from "react";

export default function Footer() {
  return (
    <footer className="bg-white border-t border-gray-200 py-16">
      <div className="max-w-7xl mx-auto px-38 sm:px-40 lg:px-48">
        <div className="flex flex-col md:flex-row justify-between space-y-8 md:space-y-0">
          <div>
            <h1 className="text-5xl tracking-widest font-light mb-4">DROBė</h1>
            <p className="text-xs tracking-widest text-gray-500">
              © 2024 DROBė. All rights reserved.
            </p>
          </div>

          <div className="">
            <h2 className="text-lg tracking-widest font-medium mb-6">
              IMPORTANT LINKS
            </h2>
            <div className="grid grid-cols-1 gap-3">
              {[
                "Production, Delivery & Returns",
                "Terms & Conditions",
                "Privacy Policy",
                "Company Details",
              ].map((link, index) => (
                <button
                  key={index}
                  className="text-left group hover:cursor-pointer hover:scale-105"
                >
                  <span className="text-xs tracking-widest font-light">
                    — {link}
                  </span>
                </button>
              ))}
            </div>
          </div>
        </div>
      </div>
    </footer>
  );
}
